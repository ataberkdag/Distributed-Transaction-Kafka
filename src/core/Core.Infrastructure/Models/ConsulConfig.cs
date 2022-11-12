namespace Core.Infrastructure.Models
{
    public class ConsulConfig
    {
        public const string Section = "Consul";
        public string Address { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
    }
}
