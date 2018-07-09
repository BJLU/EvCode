using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientPart
{
    class Client
    {
        public IPHostEntry ipHostProperty { get; private set; }
        public IPAddress addressProperty { get; private set; }
        public IPEndPoint ipServerProperty { get; private set; }
        public Socket socketProperty { get; private set; }
        public byte[] bytesProperty { get; private set; }
        public byte[] msgProperty { get; private set; }

        public delegate void SocketDel(); 

        public delegate void ConnectionDel(Socket r, IPEndPoint y);
        public event ConnectionDel ConnectionEvent;

        public delegate void SendingDel(byte[] g);
        public event SendingDel SendEvent;

        public delegate void ReceivingDel(Socket t, byte[] i);
        public event ReceivingDel ReceiveEvent;

        public delegate void CloseDel(Socket y);
        public event CloseDel CloseEvent;

        static void Main(string[] args)
        {
            Client cli = new Client();

            // defined handlers for events
            cli.ConnectionEvent += cli.CreateConnection;
            cli.SendEvent += cli.Sending;
            cli.ReceiveEvent += cli.Receiving;
            cli.CloseEvent += cli.Closing;

            SocketDel socketDel;
            socketDel = cli.CreateSocket;
            socketDel.Invoke(); // call method Invoke() for execute delegate -> 'socketDel'
        }

        private void CreateSocket() // method for opened new socket for connected to the server part
        {
            byte[] bytes = new byte[1024]; // variable for consists data

            IPHostEntry ipHost = Dns.GetHostEntry("localhost"); // defined certain host
            IPAddress address = ipHost.AddressList[0]; // defined full address
            IPEndPoint ipServer = new IPEndPoint(address, 11000); // create end point for connected to server part

            // defined parameters for current socket -> full address, type socket and protocol
            Socket socketClient = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
            // saved data to the relevant property for comfortable using data in the other methods
            this.ipHostProperty = ipHost;
            this.addressProperty = address;
            this.ipServerProperty = ipServer;
            this.socketProperty = socketClient;
            this.bytesProperty = bytes;

            // create event with TWO parameters for method -> 'CreateConnection()'
            if(ConnectionEvent != null)
                ConnectionEvent(socketProperty, ipServerProperty);
        }

        private void CreateConnection(Socket socket, IPEndPoint t) // method for connected to server part through data EndPoint
        {
            socket.Connect(ipServerProperty); // call method Connect() for connected by the data End Point

            

            Console.WriteLine("introduce your message ");
            string message = Console.ReadLine(); // saved certain string for sending subsequent

            Console.WriteLine("Socket connecting with {0} ", socket.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message); // conversion string to the bytes data
            this.msgProperty = msg; // saved in the relevant property

            // defined event with ONE parameter for method -> 'Sending()'
            if(SendEvent != null)
                SendEvent(msgProperty);
        }

        private void Sending(byte[] g) // method for sending query to the server part
        {
            int bytesSent = socketProperty.Send(g); // call method Send() for sending data bytes

            // defined event with TWO parameter
            if(ReceiveEvent != null)
                ReceiveEvent(socketProperty, bytesProperty);
        }

        private void Receiving(Socket socket, byte[] i) // method for receiving data from server part
        {
            int bytesRec = socket.Receive(bytesProperty); // receive data
            // call method 'GetString()' conversion data from bytes to string
            Console.WriteLine("\nQuery from server: {0}\n\n", Encoding.UTF8.GetString(bytesProperty, 0, bytesRec));

            // defined event with TWO parameters for method -> 'CreateConnection()'
            if(ConnectionEvent != null)
                ConnectionEvent(socketProperty, ipServerProperty);

            // defined event with ONE parameter for method -> 'Closing()'
            if(CloseEvent != null)
                CloseEvent(socketProperty);
        }

        private void Closing(Socket socket) //  method for closing current connecting with server part
        {
            socket.Shutdown(SocketShutdown.Both); // call method Shutdown() for close connected
            socket.Close(); // closed connection with server part
        }
    }
}