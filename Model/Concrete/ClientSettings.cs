using Model.Abstract;

namespace Model.Concrete
{
    /// <summary>
    /// Sadece Client'in ayarları için kullanılan Model.
    /// </summary>
    public class ClientSettings : IModel
    {
        public string HostUrl { get; set; }
        public string ComputerName { get; set; }
        public string OtherName { get; set; }
    }
}
