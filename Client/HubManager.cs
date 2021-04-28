using Microsoft.AspNetCore.SignalR.Client;
using Model.Concrete;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class HubManager
    {
        private HubConnection _hubConnection;
        private ConnectionCredential _connectionCredentials;
        public HubManager(string hostUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hostUrl)
                .WithAutomaticReconnect()
                .Build();
        }

        /// <summary>
        /// Servere bağlanmak için kullanılır.
        /// </summary>
        /// <param name="connectionCredentials"></param>
        /// <returns></returns>
        public async Task ConnectAsync(ConnectionCredential connectionCredentials)
        {
            _connectionCredentials = connectionCredentials;

            await _hubConnection.StartAsync();

            await SendConnectionCredentials(connectionCredentials);

            this.SendSignalLoop();

            Console.WriteLine("Sunucuya bağlantı sağlandı. Herhangi birşey yapmanıza gerek yok.");
        }

        /// <summary>
        /// Client Server'a ConnectionCredential nesnesini, Server'in Clienti daha detaylı tanıması için gönderir.
        /// </summary>
        /// <param name="connectionCredentials"></param>
        /// <returns></returns>
        private async Task SendConnectionCredentials(ConnectionCredential connectionCredentials)
        {
            await _hubConnection.SendAsync("SendConnectionCredential", connectionCredentials);
        }

        /// <summary>
        /// Client Server'a 5 saniyede bir sinyal gönderir.
        /// </summary>
        private async void SendSignalLoop()
        {
            int signalTime = 3;
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(signalTime));
                await _hubConnection.SendAsync("ImHere");
            }
        }
    }
}
