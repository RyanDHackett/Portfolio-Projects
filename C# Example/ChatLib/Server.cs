using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatLib
{
    /// <summary>
    /// Most of this code is taken from https://msdn.microsoft.com/en-us/library/system.net.sockets.tcplistener(v=vs.110).aspx
    /// </summary>
    public class Server : ChatApplication
    {
        private TcpListener runningServer;
        /// <summary>
        /// Getter and Setter for runningServer property
        /// </summary>
        public TcpListener RunningServer
        {
            get
            {
                return runningServer;
            }
            set
            {
                runningServer = value;
            }
        }



        /// <summary>
        /// Constructor of Server Class
        /// Instances inner TcpListener property
        /// </summary>
        public Server()
        {
            RunningServer = new TcpListener(IPAddress.Parse(Address), PortNumber);
        }

        /// <summary>
        /// Makes the RunningServer(The listener) active and starts a blocking call which listens 
        /// for a client connection
        /// </summary>
        public void ListenForClient()
        {
            // Start listening for client requests.
            RunningServer.Start();
            // Perform a blocking call to accept requests.
            RunningClient = RunningServer.AcceptTcpClient();
            // Get the server's client network stream object
            DataStream = RunningClient.GetStream();

        } //end method ListenForClient

    }
}
