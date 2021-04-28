using Model.Abstract;

namespace Model.Concrete
{
    /// <summary>
    /// Client'den Servere gönderilen, Serverin Client hakkında daha fazla bilgiye sahip olması için kullanılan Model.
    /// </summary>
    public class ConnectionCredential : IModel
    {
        public string ComputerName { get; set; }
        public string OtherName { get; set; }
    }
}
