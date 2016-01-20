using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;
using System.Net;
using System.Net.Sockets;

namespace ChatConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-server")
            {
                //run a server
                try
                {
                    //Listen for a connection from a client
                    Server server = new Server();
                    Console.WriteLine("Server started on port " + server.PortNumber);
                    Console.WriteLine("Waiting for client connection...");
                    //BLOCKING CALL
                    server.ListenForClient();
                    Console.WriteLine("Connected!\n\n");


                    StartChat(server);
                    server.RunningServer.Stop();
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                    Console.WriteLine("Application Shutting Down");
                    Console.ReadLine();
                }
            }
            else
            {
                //run a client
                Client client = new Client();
                try
                {
                    //BLOCKING CALL
                    client.Connect();//Will fail and close app if server isn't available
                    Console.WriteLine("Connected!\n\n");

                    StartChat(client);

                }
                catch (SocketException e)
                {
                    Console.WriteLine("Failed to connect to the server, exiting program.");
                    Console.ReadLine();
                }

            }
        }//end main method

        /// <summary>
        /// Generic messaging method for a client or a server
        /// </summary>
        /// <param name="app">A client or server, passed in as a ChatApplication superclass</param>
        public static void StartChat(ChatApplication app)
        {
            bool running = true;

            //While loop for listening for a client message or a key press
            while (running)
            {
                //If the data stream has a message from the client
                if (app.ReceiveMessage())
                {
                    Console.WriteLine(app.Message);
                }
                //If someone has pressed a key in the console
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    /*if the key is "i", enter input mode, where a user can type "quit" to exit the application, or
                    enter a message to send
                    */
                    if (key.Key == ConsoleKey.I)
                    {
                        Console.Write(">> ");
                        //BLOCKING CALL
                        String message = Console.ReadLine();
                        if (message.ToUpper() == "QUIT")
                        {
                            running = false;
                            break;
                        }
                        else
                        {
                            app.SendMessage(message);
                        }
                    }
                }

            }

            app.RunningClient.Close();
            Console.WriteLine("Application Shutting Down");
            Console.ReadLine();
        }//end StartChat method
    }
}



            //TcpListener server = null;
            //try
            //{
            //    // Set the TcpListener on port 13000.
            //    Int32 port = 13000;
            //    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            //    // TcpListener server = new TcpListener(port);
            //    servernghdhh = new TcpListener(localAddr, port);

            //    // Start listening for client requests.
            //    serversfsf.Start();

            //    // Buffer for reading data
            //    Byte[] bytes = new Byte[256];
            //    String data = null;

            //    // Enter the listening loop.
            //    while (true)
            //    {
            //        Console.Write("Waiting for a connection... ");

            //        // Perform a blocking call to accept requests.
            //        // You could also user server.AcceptSocket() here.
            //        TcpClient client = server.AcceptTcpClient();
            //        Console.WriteLine("Connected!");

            //        data = null;

            //        // Get a stream object for reading and writing
            //        NetworkStream stream = client.GetStream();

            //        int i;

            //        // Loop to receive all the data sent by the client.
            //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //        {
            //            // Translate data bytes to a ASCII string.
            //            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            //            Console.WriteLine("Received: {0}", data);

            //            // Process the data sent by the client.
            //            data = data.ToUpper();

            //            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            //            // Send back a response.
            //            stream.Write(msg, 0, msg.Length);
            //            Console.WriteLine("Sent: {0}", data);
            //        }

            //        // Shutdown and end connection
            //        client.Close();
        //        }
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e);
        //    }
        //    finally
        //    {
        //        // Stop listening for new clients.
        //        server.Stop();
        //    }


        //    Console.WriteLine("\nHit enter to continue...");
        //    Console.Read();
        //}

        //static void Connect(String server, String message)
        //{
        //    try
        //    {
        //        // Create a TcpClient.
        //        // Note, for this client to work you need to have a TcpServer 
        //        // connected to the same address as specified by the server, port
        //        // combination.
        //        Int32 port = 13000;
        //        TcpClient client = new TcpClient(server, port);

        //        // Translate the passed message into ASCII and store it as a Byte array.
        //        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        //        // Get a client stream for reading and writing.
        //        //  Stream stream = client.GetStream();

        //        NetworkStream stream = client.GetStream();

        //        // Send the message to the connected TcpServer. 
        //        stream.Write(data, 0, data.Length);

        //        Console.WriteLine("Sent: {0}", message);

        //        // Receive the TcpServer.response.

        //        // Buffer to store the response bytes.
        //        data = new Byte[256];

        //        // String to store the response ASCII representation.
        //        String responseData = String.Empty;

        //        // Read the first batch of the TcpServer response bytes.
        //        Int32 bytes = stream.Read(data, 0, data.Length);
        //        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        //        Console.WriteLine("Received: {0}", responseData);

        //        // Close everything.
        //        stream.Close();
        //        client.Close();
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        Console.WriteLine("ArgumentNullException: {0}", e);
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e);
        //    }

        //    Console.WriteLine("\n Press Enter to continue...");
        //    Console.Read();
        //}


