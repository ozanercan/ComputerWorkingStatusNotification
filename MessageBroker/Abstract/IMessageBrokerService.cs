namespace MessageBroker.Abstract
{
    /// <summary>
    /// MQ sistemleri için genel methodları barındıran interface.
    /// </summary>
    public interface IMessageBrokerService
    {
        /// <summary>
        /// Kuyruk oluşturmak için kullanılır.
        /// </summary>
        /// <param name="queue">Kuyruk Adı</param>
        public void GenerateQueue(string queue);

        /// <summary>
        /// Kuyruğa mesaj göndermek için kullanılır.
        /// </summary>
        /// <param name="queue">Kuyruk Adı</param>
        /// <param name="body">Mesaj</param>
        public void Send(string queue, string body);
    }
}
