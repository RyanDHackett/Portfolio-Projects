using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ChatLib;

namespace ClientUI
{
    public partial class ui : Form
    {
        /// <summary>
        /// The client object to be used by the program
        /// </summary>
        Client client;
        /// <summary>
        /// For Connecting to the server and listening for messages
        /// </summary>
        private Thread connectAndReceiveThread;
        /// <summary>
        /// For sending messages to the server
        /// </summary>
        private Thread sendMessageThread;
        /// <summary>
        /// For disconnecting from (and shutting down) the server
        /// </summary>
        private Thread disconnectThread;
        /// <summary>
        /// constructor for the client form
        /// Instantiates the inner tcpClient, then sets up all event handlers.
        /// </summary>
        public ui()
        {
            client = new Client();
            //Initialize all event handlers for the UI
            client.MessageReceived += new MessageHandler(SendOrReceiveMessage);
            client.MessageSent += new MessageHandler(SendOrReceiveMessage);
            client.Connected += new ConnectHandler(Connect);
            client.Disconnected += new DisconnectHandler(Disconnect);
            InitializeComponent();
        }

        /// <summary>
        /// Starts a new thread that sends a message to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            client.Message = txtMessage.Text;
            txtMessage.Clear();
            sendMessageThread = new Thread(client.SendMessage);
            sendMessageThread.Name = "MessageSendingThread";
            sendMessageThread.Start();
        }
        /// <summary>
        /// Starts a thread that connects to the server and starts the listening loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectAndReceiveThread = new Thread(client.Connect);
            connectAndReceiveThread.Name = "ConnectionEstablishingAndMessageReceivingThread";
            connectAndReceiveThread.Start();
        }
        /// <summary>
        /// Started by the receive and send event handlers to print a message to the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendOrReceiveMessage(object sender, MessageEventArgs e)
        {
            //if(Thread.CurrentThread.Name == "ConnectionEstablishingAndMessageReceivingThread")
            //{
            //    if(sendMessageThread != null)
            //    {
            //        sendMessageThread.Join();
            //    }
            //}

            if (txtConversation.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(
                    delegate ()
                    {
                        if (e.Message == "quit")
                        {
                            Exit();
                        }
                        else
                        { 
                            if (e.IsMessageFromServer)
                            {
                                WriteToChatBox(e.Message);
                            }
                            else
                                { 
                                    WriteToChatBox(">>" + e.Message);
                                }
                        }
                    });
                txtConversation.BeginInvoke(mi);
            }
            else
            {
                WriteToChatBox(e.Message);
            }
        }
        /// <summary>
        /// Started by the disconnect handler to disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            if (txtConversation.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(
                    delegate ()
                    {
                        WriteToChatBox(">>Disconnected");
                    });
                txtConversation.BeginInvoke(mi);
            }
            else
            {
                WriteToChatBox(">>Disconnected");
            }
            
        }
        /// <summary>
        /// Started by the connect handler to connect to the server
        /// </summary>
        public void Connect()
        {
            if (txtConversation.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(
                    delegate ()
                    {
                        WriteToChatBox(">>Connected");
                    });
                txtConversation.BeginInvoke(mi);
            }
            else
            {
                WriteToChatBox(">>Connected");
            }
        }

        /// <summary>
        /// Prints the specified message to the conversation textbox
        /// </summary>
        /// <param name="message"></param>
        public void WriteToChatBox(String message)
        {
            txtConversation.AppendText(Environment.NewLine + message);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //send the special message quit to the server which shuts down
            //the server.
            client.Message = "quit";
            sendMessageThread = new Thread(client.SendMessage);
            sendMessageThread.Name = "MessageSendingThread";
            sendMessageThread.Start();
            //wait until the message sends to the server before allowing more text entry..
            sendMessageThread.Join();
            disconnectThread = new Thread(client.Disconnect);
            disconnectThread.Name = "DisconnectingThread";
            disconnectThread.Start();
        }
        /// <summary>
        /// Gracefully ends all threads and stops the server
        /// </summary>
        public void Exit()
        {
            //send the special message quit to the server which shuts down
            //the server.
            client.Message = "quit";
            sendMessageThread = new Thread(client.SendMessage);
            sendMessageThread.Name = "MessageSendingThread";
            sendMessageThread.Start();
            //wait until the message sends to the server before closing the app..
            sendMessageThread.Join();
            //Stop listening for messages
            disconnectThread = new Thread(client.Disconnect);
            disconnectThread.Name = "DisconnectingThread";
            disconnectThread.Start();
            //if the listening loop is active when the server closes,
            //exceptions could happen. So we need to wait until the
            //listening process finishes closing
            disconnectThread.Join();

            //stop the TcpClient and write the log to file
            client.Close();
        }

        private void ui_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
            //Close the form from this alternate control
            this.Close();
        }
    }
}
