using Model.Abstract;
using System;

namespace Model.Concrete
{
    /// <summary>
    /// Serverin Clientden gelen Model ile Birleştirilmiş Detaylı Bilgi Modelidir.
    /// </summary>
    public class ModifiedConnectionCredential : ConnectionCredential, IModel
    {
        public string ConnectionId { get; set; }
        public DateTime LastReceivedSignalDateTime { get; set; }
    }
}
