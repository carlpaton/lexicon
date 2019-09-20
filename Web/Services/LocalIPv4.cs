
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Web.Services
{
    /// <summary>
    /// Hack used with AWS ESC (Fargate)
    /// 
    /// Assuming the lexicon-web and lexicon-sql containers had the same IP this function is then used to help update the database connection string.
    /// `@@MACHINE_NAME@@` or what ever is string replaced with the correct IP.
    /// 
    /// Not sure why we cannot use DHCP here and just use the container names like in `docker-compose`?
    /// </summary>
    public interface ILocalIPv4
    {
        string GetLocalIPv4(NetworkInterfaceType _type);
    }
    
    public class LocalIPv4 : ILocalIPv4
    {
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
    }
}
