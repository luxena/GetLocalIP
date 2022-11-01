using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GetLocalIP
{
    class Program
    {
        static void Main(string[] args)
        {

           
            var ethernet = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            var wifi = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            Console.WriteLine(wifi);
            IsConnected(NetworkInterfaceType.Wireless80211);
           

           
            //GetIP();
            Console.ReadLine();
        }

        public static string GetLocalIPv4(NetworkInterfaceType _type)
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

        static void IsConnected(NetworkInterfaceType _type)
        {
            var isConnected = false;
            //System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                     isConnected =
            System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                }
            }


            Console.WriteLine(isConnected);
        }
        static void GetIP()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
                Console.WriteLine(localIP);
            }

        }
        static void GetAllIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());

            Console.WriteLine(host);

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {

                    Console.WriteLine(ip);
                    localIP = ip.ToString();
                    Console.WriteLine(localIP);
                }
            }
        }
    }
}
