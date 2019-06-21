
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Web.Services
{
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
