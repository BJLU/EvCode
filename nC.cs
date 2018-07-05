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

            cli.ConnectionEvent += cli.CreateConnection;
            cli.SendEvent += cli.Sending;
            cli.ReceiveEvent += cli.Receiving;
            cli.CloseEvent += cli.Closing;

            SocketDel socketDel;
            socketDel = cli.CreateSocket;
            socketDel.Invoke();
        }

        private void CreateSocket()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress address = ipHost.AddressList[0];
            IPEndPoint ipServer = new IPEndPoint(address, 11000);

            Socket socketClient = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
            this.ipHostProperty = ipHost;
            this.addressProperty = address;
            this.ipServerProperty = ipServer;
            this.socketProperty = socketClient;
            this.bytesProperty = bytes;

            if(ConnectionEvent != null)
                ConnectionEvent(socketProperty, ipServerProperty);
        }

        private void CreateConnection(Socket socket, IPEndPoint t)
        {
            socket.Connect(ipServerProperty);

            

            Console.WriteLine("introduce your message ");
            string message = Console.ReadLine();

            Console.WriteLine("Socket connecting with {0} ", socket.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);
            this.msgProperty = msg;

            if(SendEvent != null)
                SendEvent(msgProperty);
        }

        private void Sending(byte[] g)
        {
            int bytesSent = socketProperty.Send(g);

            if(ReceiveEvent != null)
                ReceiveEvent(socketProperty, bytesProperty);
        }

        private void Receiving(Socket socket, byte[] i)
        {
            int bytesRec = socket.Receive(bytesProperty);
            Console.WriteLine("\nQuery from server: {0}\n\n", Encoding.UTF8.GetString(bytesProperty, 0, bytesRec));

            if(ConnectionEvent != null)
                ConnectionEvent(socketProperty, ipServerProperty);


            if(CloseEvent != null)
                CloseEvent(socketProperty);
        }

        private void Closing(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}