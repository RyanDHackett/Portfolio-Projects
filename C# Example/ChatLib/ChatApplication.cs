using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public abstract class ChatApplication
    {
        private const String address = "127.0.0.1";
        private const int portNumber = 13000;
        private String message;
        private TcpClient runningClient;
        private NetworkStream dataStream;

        /// <summary>
        /// Getter and Setter for runningClient property
        /// </summary>
        public TcpClient RunningClient
        {
            get
            {
                return runningClient;
            }
            set
            {
                runningClient = value;
            }
        }
        /// <summary>
        /// Getter and Setter for address property
        /// </summary>
        public String Address
        {
            get
            {
                return address;
            }
        }
        /// <summary>
        /// Getter and Setter for portNumber property
        /// </summary>
        public int PortNumber
        {
            get
            {
                return portNumber;
            }
        }

        /// <summary>
        /// Getter and Setter for dataStream property
        /// </summary>
        public NetworkStream DataStream
        {
            get
            {
                return dataStream;
            }
            set
            {
                dataStream = value;
            }
        }
        /// <summary>
        /// Getter and Setter for message property
        /// </summary>
        public String Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        /// <summary>
        /// Writes the provided message to the datastream shared between the client and the server. 
        /// Server can read it immedietey after.
        /// </summary>
        /// <param name="message">The message to be sent to the Client or Server</param>
        public void SendMessage(String message)
        {
            byte[] messageData = System.Text.Encoding.ASCII.GetBytes(message);

            DataStream.Write(messageData, 0, messageData.Length);
        }//end method SendMessage

        /// <summary>
        /// Reads data from the provided NetworkStream after confirming that data can be received
        /// </summary>
        /// <param name="DataStream"></param>
        /// <returns></returns>
        public bool ReceiveMessage()
        {
            if (DataStream.DataAvailable)
            {
                Byte[] messageData = new Byte[256];
                String receievedMessage = null;

                // Write the received data into the messageData array, leaving blank bytes where they weren't filled
                DataStream.Read(messageData, 0, messageData.Length);
                // Translate data bytes to an ASCII string.
                receievedMessage = System.Text.Encoding.ASCII.GetString(messageData);

                //Cuts off blank bytes from the message
                String[] parts = receievedMessage.Split('\0');

                Message = parts[0];
                return true;
            }
            else
                return false;
        }//end method ReceiveMessage


    }
}
