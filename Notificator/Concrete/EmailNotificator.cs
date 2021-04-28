using MessageBroker.Abstract;
using Model.Concrete;
using Notificator.Abstract;
using System;

namespace Notificator.Concrete
{
    public class EmailNotificator : INotificator
    {
        IMessageBrokerService _messageBrokerService;
        public EmailNotificator(IMessageBrokerService messageBrokerService)
        {
            _messageBrokerService = messageBrokerService;
        }
        public void Inform(params ModifiedConnectionCredential[] modifiedConnectionCredentials)
        {
            foreach (var item in modifiedConnectionCredentials)
            {
                _messageBrokerService.GenerateQueue("emailNotify");
                _messageBrokerService.Send("emailNotify", $"{DateTime.Now}: {item.ComputerName}-{item.OtherName} adlı bilgisayardan haber alınamıyor. En son haber alma zamanı: {item.LastReceivedSignalDateTime}");
            }
        }
    }
}
