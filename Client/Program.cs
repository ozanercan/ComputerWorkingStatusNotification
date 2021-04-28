using Bogus;
using Microsoft.AspNetCore.SignalR.Client;
using Model.Concrete;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static ClientSettings _clientSettings;
        static async Task Main(string[] args)
        {
            CheckSettings();

            HubManager connector = new HubManager(_clientSettings.HostUrl);

            var connectionCredentials = new ConnectionCredential()
            {
                ComputerName = _clientSettings.ComputerName,
                OtherName = _clientSettings.OtherName
            };

            Console.Title = $"{connectionCredentials.OtherName} adıyla bağlandı.";

            await connector.ConnectAsync(connectionCredentials);

            Console.ReadKey();
        }

        /// <summary>
        /// settings.json kontrol edilir, yoksa oluşturulur ve ayarların girilmesi istenir.
        /// </summary>
        static void CheckSettings()
        {
            string settingsJsonUrl = $@"{Environment.CurrentDirectory}\settings.json";

            CheckFileCreateIfNoFileExist(settingsJsonUrl);

            _clientSettings = JsonConvert.DeserializeObject<ClientSettings>(File.ReadAllText(settingsJsonUrl));

            if (_clientSettings.ComputerName == null || _clientSettings.OtherName == null || _clientSettings.HostUrl == null)
            {
                _clientSettings = SetSettings();

                File.WriteAllText(settingsJsonUrl, JsonConvert.SerializeObject(_clientSettings));

                Console.WriteLine("Konfigürasyon tamamlandı.");

                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        /// <summary>
        /// settings.json dosyası kontrol edilir, yoksa oluşturulur ve içerisinde null değerleri olan model eklenir.
        /// </summary>
        /// <param name="url">settings.json dosya urlsi</param>
        static void CheckFileCreateIfNoFileExist(string url)
        {
            if (!File.Exists(url))
            {
                var nullModel = new ClientSettings();
                using (StreamWriter sw = File.CreateText(url))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(nullModel));
                }
            }
        }

        /// <summary>
        /// Gerekli ayarların UI'dan istendiği bölümdür.
        /// </summary>
        /// <returns></returns>
        static ClientSettings SetSettings()
        {
            Console.Title = "KONFİGURASYON";
            Console.Write("Host Url: ");
            string hostUrl = Console.ReadLine();

            Console.Write("\nBilgisayarınızın Takma Adı: ");
            string otherName = Console.ReadLine();

            return new ClientSettings()
            {
                HostUrl = hostUrl,
                OtherName = otherName,
                ComputerName = Environment.MachineName,
            };
        }
    }
}
