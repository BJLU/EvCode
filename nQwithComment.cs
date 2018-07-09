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

        private void OpenSocket() // method for opened socket to client
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost"); // emphasis on certain host
            IPAddress ipAddr = ipHost.AddressList[0]; // selected full address
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000); // create data for end point connected

            // opened socket with certain data -> endPoint, Type Socket, type Protocol
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp); 

            // save data in relevent properties for comfortable using in other methods
            this.ipHostProperty = ipHost;
            this.ipAddressProperty = ipAddr;
            this.ipEndPointProperty = ipEndPoint;
            this.listenerProperty = sListener;

            // defined event with TWO parameters for method -> AddBind()
            if (BindEvent != null)
                BindEvent(ipEndPointProperty, listenerProperty);
        }

        private void AddBind(IPEndPoint ipEndP, Socket listener) // method for attaching nema for current socket
        {
            Console.WriteLine("choto");
            listener.Bind(ipEndP); // call Bind() for named current socket

            // create event with ONE argument for 'Listening()' method
            if (ListenEvent != null)
                ListenEvent(listenerProperty);
        }

        private void Listening(Socket listener) // method for listening socket to connected client part
        {
            listener.Listen(10); // called method Listen() for waiting client query

            // defined event with Two arguments for method - 'Accepting()'
            if (AcceptEvent != null)
                AcceptEvent(ipEndPointProperty, listenerProperty);
        }

        private void Accepting(IPEndPoint ipEndP, Socket listener) // method for waiting client part through End Point
        {
            Console.WriteLine("jdu connect through port {0}", ipEndP);

            Socket accepted = listener.Accept(); // connected with client through method -> 'Accept()'
            string data = null;

            // saved data to relevant property for comfortable using current data
            this.acceptedProperty = accepted;
            this.dataProperty = data;

            // create event woth TWO parameters for method -> 'Receiving()'
            if (RecEvent != null)
                RecEvent(acceptedProperty, dataProperty);

            //create event with TWO parameters for method -> 'Sending()'
            if (SendEvent != null)
                SendEvent(acceptedProperty, dataProperty);


            //create event with ONE parameter for method -> 'CloseProcesses()'
            if (CloseEvent != null)
                CloseEvent(accepted);
        }

        private void CloseProcesses(Socket accept) // method for disconneted with current client
        {
            accept.Shutdown(SocketShutdown.Both); // called Shutdown() for disconnecting
            accept.Close(); // call method -> Close() for ended connecting
        }

        private void Receiving(Socket acc, string dat) // method for receiving data from client part
        {
            byte[] bytes = new byte[1024]; // variable for saved receving data
            int bytesRec = acc.Receive(bytes); // receive data from bytes

            dat += Encoding.UTF8.GetString(bytes, 0, bytesRec); // conversion all bytes in the string
            this.dataProperty = dat; // saved data in the relevant property

            Console.WriteLine("Received information: " + dat + "\n\n"); // output certain data
        }

        private void Sending(Socket acc, string dat) // method for sending data to the client part
        {
            string reply = "thanks for your query at the -> " + dat.Length.ToString() + " simbols"; // data which displayed in the client part
            byte[] msg = Encoding.UTF8.GetBytes(reply); // conversion certain string to the bytes
            acc.Send(msg); // call method Send() for sending bytes data to the client part

            // create event with TWO parameters for method -> 'Accepting()'
            if(AcceptEvent != null)
                AcceptEvent(ipEndPointProperty, listenerProperty);
        }

    }
}