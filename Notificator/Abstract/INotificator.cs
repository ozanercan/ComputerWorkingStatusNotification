using Model.Concrete;

namespace Notificator.Abstract
{
    /// <summary>
    /// Bilgi gönderme kanallarını ayırmak için kullanılır.
    /// </summary>
    /// <param name="modifiedConnectionCredentials"></param>
    public interface INotificator
    {
        /// <summary>
        /// Bilgiyi göndermek için kullanılır.
        /// </summary>
        /// <param name="modifiedConnectionCredentials"></param>
        public void Inform(params ModifiedConnectionCredential[] modifiedConnectionCredentials);
    }
}
