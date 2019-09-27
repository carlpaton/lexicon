using System.Net;

namespace Web.Services
{
    /// <summary>
    /// Hack used with AWS ESC
    /// 
    /// The internal DNS didnt work for containers and I got over it.
    /// https://stackoverflow.com/questions/3253701/get-public-external-ip-address
    /// </summary>
    public interface IPublicIP
    {
        string GetPublicIP();
    }

    public class PublicExternalIP : IPublicIP
    {
        public string GetPublicIP()
        {
            return new WebClient()
                .DownloadString("http://icanhazip.com")
                .Trim();
        }
    }
}
