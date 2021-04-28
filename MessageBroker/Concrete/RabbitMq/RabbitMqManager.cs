using MessageBroker.Abstract;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MessageBroker.Concrete.RabbitMq
{
    public class RabbitMqManager : IMessageBrokerService
    {
        static ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        public RabbitMqManager()
        {
        }

        /// <summary>
        /// Kuyruk oluşturur.
        /// </summary>
        /// <param name="queue">Kuyruk Adı</param>
        public void GenerateQueue(string queue)
        {
            try
            {
                using (IConnection connection = connectionFactory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Kuyruğa mesaj gönderir.
        /// </summary>
        /// <param name="queue">Kuyruk Adı</param>
        /// <param name="body">Mesaj</param>
        public void Send(string queue, string body)
        {
            try
            {
                using (IConnection connection = connectionFactory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    byte[] bytemessage = Encoding.UTF8.GetBytes(body);
                    channel.BasicPublish(exchange: "", routingKey: queue, body: bytemessage, basicProperties: null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
