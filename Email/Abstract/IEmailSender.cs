namespace Email.Abstract
{
    public interface IEmailSender
    {
        /// <summary>
        /// E-Mail göndermek için kullanılır.
        /// </summary>
        /// <param name="message">Mail içeriği</param>
        public void Send(string message);
    }
}
