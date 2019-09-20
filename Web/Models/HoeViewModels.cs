namespace Web.Models
{
    public class HoeViewModel
    {
        public string EnvSql { get; set; }
        public string ConfSql { get; set; }
        public string HostIp { get; set; }
        public string PublicIp { get; set; }
        public string ActualConnectionString { get; set; }
        public string EnvSubstituteLocalIp { get; set; }
        public string EnvSubstitutePublicIp { get; set; }
    }
}
