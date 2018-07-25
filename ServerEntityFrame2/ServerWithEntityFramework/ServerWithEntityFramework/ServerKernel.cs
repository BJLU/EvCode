using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerWithEntityFramework
{
    class ServerKernel // class for define common functions for server part
    {
        // i created this properties for comfortable access to the next data
        public static IPHostEntry IpHostProperty { get; private set; } // property for ipHost
        public static IPAddress IpAddressProperty { get; private set; } // property for ipAddr
        public static IPEndPoint IpEndPointProperty { get; private set; } // property for ipEndPoint
        public static Socket ListenerProperty { get; private set; } // property for sListener
        public static Socket HandlerProperty { get; private set; } // property for handler

        // i created delegate and events for simple organization interaction between program's functions
        public delegate void RootCommunity(); // delegate for next events
        public event RootCommunity AddEvent; // this event for call function 'AddUser'
        public event RootCommunity DelEvent; // this event for call function 'DeleteUser'
        public event RootCommunity ExitEvent; // this event for call function 'ExitProgram'

        internal void DefinedHostComputer() // method for define certain host
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost"); // defined certain host -> 'localhost'

            if (ipHost == null) // if got unsuccessful created certain host
            {
                Console.WriteLine("ipHost is null");
            }
            else
            {
                IpHostProperty = ipHost; // copy current data to the property

            }
        }

        internal void DefinedAddress() // method for get full address current machine
        {
            IPAddress ipAddr = IpHostProperty.AddressList[0]; // got full address

            if (ipAddr == null) // if got something error then
            {
                Console.WriteLine("ipAddr is null");
            }
            else
            {
                IpAddressProperty = ipAddr; // copy current data to the property
            }
        }

        internal void DefinedEndPoint() // method for assembly all data about chosen connecting point
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IpAddressProperty, 11000); // got all data about chosen connecting point

            if (ipEndPoint == null) // if got some errors
            {
                Console.WriteLine("ipEndPoint is null");
            }
            else
            {
                IpEndPointProperty = ipEndPoint; // copy current data to the property
            }
        }

        internal void OpenSocket() // function for opening socket for get query to the connection from client part
        {
            Socket sListener = new Socket(IpAddressProperty.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // opening current socket

            if (sListener == null) // if have something error then
            {
                Console.WriteLine("sListener is null");
            }
            else
            {
                ListenerProperty = sListener; // copy data to the current property
            }
        }

        internal void NamingSocket() // function for communicating with client part
        {
            ListenerProperty.Bind(IpEndPointProperty); // initializing data about client part
            ListenerProperty.Listen(10); // opened port for connecting

            while (true)
            {
                Console.WriteLine($"waiting connecting through the port {IpEndPointProperty}");

                Socket handler = ListenerProperty.Accept(); // waiting to the client part
                if (handler == null) // if have something errors then
                {
                    Console.WriteLine("handler is null");
                }
                else
                {
                    HandlerProperty = handler; // copy data to the current property
                }

                string data = null; // variable for content communications data between server and client part

                byte[] bytes = new byte[1024]; // variable for got something data from client part
                int bytesRec = handler.Receive(bytes); // performing got data

                data += Encoding.UTF8.GetString(bytes, 0, bytesRec); // convert get data to the string type

                Console.WriteLine($"Receiving command: {data} \n\n");

                ValidationCommands(data); // send receiving data to the method for scanning to something commands
                //start entity



                string reply = $"query is performed -> length {data.Length.ToString()}"; // preparation data for sending to the client part

                byte[] msg = Encoding.UTF8.GetBytes(reply); // convert current data in bytes for sending
                handler.Send(msg); // sending current data to the client part
            }
        }

        public void ValidationCommands(string datA) // function for detecting next commands
        {
            if (datA.Equals("add"))
            {
                Console.WriteLine("call add");

                if (AddEvent != null) // call event for function -> 'StartPoint.AddUser'
                {
                    AddEvent();
                }
            }
            else if (datA.Equals("del"))
            {
                Console.WriteLine("call del");

                if (DelEvent != null) // call event for method -> 'StartPoint.DeleteUser'
                {
                    DelEvent();
                }
            }
            else if (datA.Equals("exit"))
            {
                Console.WriteLine("call exit");

                if (ExitEvent != null) // call event for method -> 'StartPoint.ExitProgram'
                {
                    ExitEvent();
                }
            }
        }

        // i created it's two methods for saving laconic code size
        internal string ReceivingData() // method for defined receiving query from client part
        {
            Socket handlerRefresh = ListenerProperty.Accept(); // defined accept client query

            if (handlerRefresh != null) // validation current variable
            {
                HandlerProperty = handlerRefresh; // save data to the certain property
            }
            else
            {
                Console.WriteLine("handlerRefresh is null"); // out put message
            }

            string dataCom = null; // variable for receiving data
            byte[] bytes = new byte[1024]; // variable for receiving data
            int bytesRec = HandlerProperty.Receive(bytes); // will got all data
            dataCom += Encoding.UTF8.GetString(bytes, 0, bytesRec); // got data in string type

            return dataCom; // transfer data
        }

        internal void SendingData(string str) // method for sending data to the client part
        {
            byte[] message = Encoding.UTF8.GetBytes(str); // got all data in byte massive type
            HandlerProperty.Send(message); // sending message
        }
    }
}
