using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    /// <summary>
    /// Most of this code is taken from https://msdn.microsoft.com/en-us/library/system.net.sockets.tcplistener(v=vs.110).aspx
    /// </summary>
    public class Client : ChatApplication
    {
        /// <summary>
        /// Constructor of Client Class
        /// Instances inner TcpClient property
        /// </summary>
        public Client()
        {
            RunningClient = new TcpClient();
        }
        /// <summary>
        /// Attempts to connect to a server, can fail.
        /// Gets the network stream for the now connected client
        /// </summary>
        public void Connect()
        {
            RunningClient.Connect(IPAddress.Parse(Address), PortNumber);
            DataStream = RunningClient.GetStream();
        }



    }
}
