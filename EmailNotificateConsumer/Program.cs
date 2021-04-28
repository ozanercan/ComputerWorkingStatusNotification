using Email.Abstract;
using Email.Concrete;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace EmailNotificateConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
        start:
            Console.Clear();
            try
            {
                IEmailSender emailSender = new GmailEmailSender();

                ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connection = connectionFactory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    Console.WriteLine("E-Mail gönderici aktif edildi.");

                    channel.QueueDeclare(queue: "emailNotify", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                                    {
                                        var body = ea.Body.ToArray();
                                        var message = Encoding.UTF8.GetString(body);
                                        emailSender.Send(message);
                                    };
                    channel.BasicConsume(queue: "emailNotify",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine("E-Mail Göndericisini kapatmak için bir tuşa basın.");
                    Console.ReadKey();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Sistemde bir hata algılandı. Tekrar bağlanmaya çalışılıyor.");
                Thread.Sleep(1000);
                goto start;
            }

        }
    }
}