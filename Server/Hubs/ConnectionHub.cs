using Microsoft.AspNetCore.SignalR;
using Model.Concrete;
using Notificator.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Hubs
{
    public class ConnectionHub : Hub
    {
        INotificator _notificator;

        static Timer timer = new Timer();


        int signalWarningTime = 10;

        /// <summary>
        /// Servere göre uyarlanmış Connection Credential listesidir.
        /// </summary>
        static List<ModifiedConnectionCredential> _modifiedConnectionCredentials = new List<ModifiedConnectionCredential>();

        public ConnectionHub(INotificator notificator)
        {
            _notificator = notificator;

            SetTimerConfigurations();
        }

        /// <summary>
        /// Timer ile ilgili ayarları barındırır ve düzenler.
        /// </summary>
        private void SetTimerConfigurations()
        {
            timer.Interval = 10000;

            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                CheckConnectionTimes();
            };

            timer.Start();
        }


        /// <summary>
        /// Client'den gelen ConnectionCredential'ı servere göre düzenlenen ModifiedConnectionCredential listesine aktarır.
        /// </summary>
        /// <param name="connectionCredentials"></param>
        [HubMethodName("SendConnectionCredential")]
        public void SendConnectionCredential(ConnectionCredential connectionCredentials)
        {
            var modifiedConnectionCredentialToAdd = new ModifiedConnectionCredential
            {
                ComputerName = connectionCredentials.ComputerName,
                OtherName = connectionCredentials.OtherName,
                ConnectionId = Context.ConnectionId,
                LastReceivedSignalDateTime = DateTime.Now
            };

            _modifiedConnectionCredentials.Add(modifiedConnectionCredentialToAdd);
        }

        /// <summary>
        /// Client'den n saniyede bir gelen sinyaldır. Her sinyalde Client'in son bağlanma süresini düzenler.
        /// </summary>
        [HubMethodName("ImHere")]
        public void ImHere()
        {
            Console.WriteLine(Context.ConnectionId);
            var modifiedConnectionCredentialByClient = _modifiedConnectionCredentials.Where(p => p.ConnectionId == Context.ConnectionId);

            if (modifiedConnectionCredentialByClient.Any())
                modifiedConnectionCredentialByClient.First().LastReceivedSignalDateTime = DateTime.Now;
        }

        /// <summary>
        /// Bağlantı zamanlarını kontrol eder ve n süresinden uzun sinyal göndermeyen clientleri bulur ve bilgilendiriciye gönderir.
        /// </summary>
        public void CheckConnectionTimes()
        {
            
            var connectionLoseClients = _modifiedConnectionCredentials.Where(p => (DateTime.Now - p.LastReceivedSignalDateTime).Seconds > signalWarningTime);

            if (connectionLoseClients.Any())
            {
                var connectionLosesToInform = connectionLoseClients.ToArray();
                _notificator.Inform(connectionLosesToInform);

                for (int i = 0; i < connectionLoseClients.Count(); i++)
                {
                    _modifiedConnectionCredentials.Remove(connectionLoseClients.ElementAt(i));
                }
            }
        }

        public async override Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId + " bağlandı.");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var disconnectedClient = _modifiedConnectionCredentials.Where(p => p.ConnectionId == Context.ConnectionId);
            if (disconnectedClient.Any())
                Console.WriteLine($"{disconnectedClient.First().ComputerName} - {disconnectedClient.First().OtherName} bağlantısı kesildi.");
            else
                Console.WriteLine($"{Context.ConnectionId} bağlantısı kesildi.");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
