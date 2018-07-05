using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class ServerCore
    {

        public IPHostEntry ipHostProperty { get; private set; }
        public IPAddress ipAddressProperty { get; private set; }
        public IPEndPoint ipEndPointProperty { get; private set; }
        public Socket listenerProperty { get; private set; }
        public Socket acceptedProperty { get; private set; }
        public string dataProperty { get; private set; }

        public delegate void OpenLink();

        public delegate void BindingDel(IPEndPoint ipE, Socket listener);
        public event BindingDel BindEvent;

        public delegate void ListeningEvent(Socket i);
        public event ListeningEvent ListenEvent;

        public delegate void AcceptingEvent(IPEndPoint r, Socket y);
        public event AcceptingEvent AcceptEvent;

        public delegate void CloseProcessesEvent(Socket y);
        public event CloseProcessesEvent CloseEvent;

        public delegate void ReceiveDel(Socket y, string d);
        public event ReceiveDel RecEvent;

        public delegate void SendDel(Socket t, string s);
        public event SendDel SendEvent;

        public static void Main()
        {
            ServerCore core = new ServerCore();

            core.BindEvent += core.AddBind;
            core.ListenEvent += core.Listening;
            core.AcceptEvent += core.Accepting;
            core.CloseEvent += core.CloseProcesses;
            core.RecEvent += core.Receiving;
            core.SendEvent += core.Sending;

            OpenLink linker;
            linker = core.OpenSocket;


            linker.Invoke();

        }

        private void OpenSocket()
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            this.ipHostProperty = ipHost;
            this.ipAddressProperty = ipAddr;
            this.ipEndPointProperty = ipEndPoint;
            this.listenerProperty = sListener;

            if (BindEvent != null)
                BindEvent(ipEndPointProperty, listenerProperty);
        }

        private void AddBind(IPEndPoint ipEndP, Socket listener)
        {
            Console.WriteLine("choto");
            listener.Bind(ipEndP);


            if (ListenEvent != null)
                ListenEvent(listenerProperty);
        }

        private void Listening(Socket listener)
        {
            listener.Listen(10);

            if (AcceptEvent != null)
                AcceptEvent(ipEndPointProperty, listenerProperty);
        }

        private void Accepting(IPEndPoint ipEndP, Socket listener)
        {
            Console.WriteLine("jdu connect through port {0}", ipEndP);

            Socket accepted = listener.Accept();
            string data = null;

            this.acceptedProperty = accepted;
            this.dataProperty = data;


            if (RecEvent != null)
                RecEvent(acceptedProperty, dataProperty);

            if (SendEvent != null)
                SendEvent(acceptedProperty, dataProperty);



            if (CloseEvent != null)
                CloseEvent(accepted);
        }

        private void CloseProcesses(Socket accept)
        {
            accept.Shutdown(SocketShutdown.Both);
            accept.Close();
        }

        private void Receiving(Socket acc, string dat)
        {
            byte[] bytes = new byte[1024];
            int bytesRec = acc.Receive(bytes);

            dat += Encoding.UTF8.GetString(bytes, 0, bytesRec);
            this.dataProperty = dat;

            Console.WriteLine("Received information: " + dat + "\n\n");
        }

        private void Sending(Socket acc, string dat)
        {
            string reply = "thanks for your query at the -> " + dat.Length.ToString() + " simbols";
            byte[] msg = Encoding.UTF8.GetBytes(reply);
            acc.Send(msg);

            if(AcceptEvent != null)
                AcceptEvent(ipEndPointProperty, listenerProperty);
        }

    }
}