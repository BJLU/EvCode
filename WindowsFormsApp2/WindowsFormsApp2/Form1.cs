using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Data.Entity;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        // i created this properties for comfortable access to the next data
        public IPHostEntry HostProperty { get; private set; } // property for ipHost
        public IPAddress AddressProperty { get; private set; } // property for ipAddr
        public IPEndPoint EndPointProperty { get; private set; } // property for ipEndPoint
        public Socket ListenerProperty { get; private set; } // property for sListener
        public Socket HandlerProperty { get; private set; } // property for handler

        // i created delegate and events for simple organization interaction between program's functions
        public delegate void RootCommunity(); // delegate for next events
        public event RootCommunity AddEvent; // this event for call function 'AddUser'
        public event RootCommunity DelEvent; // this event for call function 'DeleteUser'
        public event RootCommunity ExitEvent; // this event for call function 'ExitProgram'

        internal delegate void ShowUsersFromDB(DbSet<User> users);
        internal event ShowUsersFromDB UsersEvent; // event for performing users from database

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OutputData(object sender, EventArgs e)
        {

        }

        private void InitializePerformers()
        {
            Form1 form1 = new Form1();
            form1.AddEvent += AddUser; // binding performed for event AddEvent
            form1.DelEvent += DeleteUser; // binding performed for event DelEvent
            form1.ExitEvent += ExitProgram; // binding performed for current event ExitEvent

            form1.UsersEvent += form1.ShowUsers;
        }

        private void OpenSocket(object sender, EventArgs e)
        {
            InitializePerformers();
            DefinedHostComputer();
            AddressComputer();
            DataEndPoint();
            OpenSocketData();
        }
        private void NamingSocket_Click(object sender, EventArgs e)
        {
            NamingSocketForClient();
        }
        private void DefinedHostComputer() // method for define certain host
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost"); // defined certain host -> 'localhost'

            if (ipHost != null) // if got unsuccessful created certain host
            {
                HostProperty = ipHost; // copy current data to the property
                ShowMarker("host data -> created\n");
            }
            else
            {
                ShowMarker("host->error\n");
            }
        }
        private void AddressComputer() // method for get full address current machine
        {
            IPAddress ipAddr = HostProperty.AddressList[0]; // got full address

            if (ipAddr != null) // if got something error then
            {
                AddressProperty = ipAddr; // copy current data to the property
                ShowMarker("address -> created\n");
            }
            else
            {
                ShowMarker("address->error\n");
            }
        }
        private void DataEndPoint() // method for assembly all data about chosen connecting point
        {
            IPEndPoint ipEndPoint = new IPEndPoint(AddressProperty, 11000); // got all data about chosen connecting point

            if (ipEndPoint != null)
            {
                EndPointProperty = ipEndPoint; // copy current data to the property
                ShowMarker("EndPointData -> created\n");
            }
            else
            {
                ShowMarker("EndPoint->error\n");
            }
        }
        private void OpenSocketData() // function for opening socket for get query to the connection from client part
        {
            Socket sListener = new Socket(AddressProperty.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // opening current socket

            if (sListener != null)
            {
                ListenerProperty = sListener; // copy data to the current property
                ShowMarker("socket -> created\n");
            }
            else
            {
                ShowMarker("socket->error\n");
            }
        }
        private void NamingSocketForClient() // function for communicating with client part
        {
            ListenerProperty.Bind(EndPointProperty); // initializing data about client part
            ShowMarker("socket -> Binded\n");
            ListenerProperty.Listen(10); // opened port for connecting
            ShowMarker("socket -> listen\n");

            while(true)
            {
                ShowMarker($"waiting connecting through the port {EndPointProperty}\n");

                Socket handler = ListenerProperty.Accept(); // waiting to the client part
                if (handler != null)
                {
                    HandlerProperty = handler; // copy data to the current property
                    ShowMarker("connected -> client part\n");
                }
                else
                {
                    ShowMarker("connecting->error\n");
                }

                string data = null; // variable for content communications data between server and client part

                byte[] bytes = new byte[1024]; // variable for got something data from client part
                int bytesRec = handler.Receive(bytes); // performing got data
                ShowMarker("receiving data from client part\n");

                data += Encoding.UTF8.GetString(bytes, 0, bytesRec); // convert get data to the string type

                ShowMarker($"Receiving command: {data} \n\n");

                ValidationCommands(data); // send receiving data to the method for scanning to something commands
                //start entity

                string reply = $"query is performed -> length {data.Length.ToString()}"; // preparation data for sending to the client part

                byte[] msg = Encoding.UTF8.GetBytes(reply); // convert current data in bytes for sending
                handler.Send(msg); // sending current data to the client part
                ShowMarker("message -> sended\n");

            }
        }
        private void ValidationCommands(string data) // function for detecting next commands
        {
            if(data.Equals("add"))
            {
                ShowMarker("call add\n");

                if(AddEvent != null) // call event for function -> 'AddUser'
                {
                    AddEvent();
                }
            }
            else if(data.Equals("del"))
            {
                ShowMarker("call del");

                if(DelEvent != null) // call event for method -> 'DeleteUser'
                {
                    DelEvent();
                }
            }
            else if(data.Equals("exit"))
            {
                ShowMarker("call exit");

                if(ExitEvent != null) // call event for method -> 'ExitProgram'
                {
                    ExitEvent();
                }
            }
        }
        // i created it's two methods for saving laconic code size
        private string ReceivingData() // method for defined receiving query from client part
        {
            Socket handlerRefresh = ListenerProperty.Accept(); // defined accept client query

            if (handlerRefresh != null) // validation current variable
            {
                HandlerProperty = handlerRefresh; // save data to the certain property
            }
            else
            {
                ShowMarker("handlerRefresh is null\n"); // out put message
            }

            string dataCom = null; // variable for receiving data
            byte[] bytes = new byte[1024]; // variable for receiving data
            int bytesRec = HandlerProperty.Receive(bytes); // will got all data
            dataCom += Encoding.UTF8.GetString(bytes, 0, bytesRec); // got data in string type

            return dataCom; // transfer data
        }
        // method for sending data to the client part
        private void SendingData(string str)
        {
            byte[] message = Encoding.UTF8.GetBytes(str); // got all data in byte massive type
            HandlerProperty.Send(message); // sending message
        }

        private static void AddUser() // method for defined add new User to the database
        {
            Form1 form = new Form1();
            form.SendingData("introduce User's Name\n"); // call method SendingData for send current string
            var userName = form.ReceivingData(); // call method Receiving for got data from client part
            string userN = userName.ToString(); // convert get data to the type string

            form.SendingData("introduce User's Age"); // call method SendingData for send current string
            var userAge = form.ReceivingData(); // call method ReceivingData for get all data from client part
            int userA = Int32.Parse(userAge); // convert get data in the int type

            using (UserContext db = new UserContext()) // defined variable about class UserContext
            {
                User newUser = new User { Name = userN, Age = userA }; // defined instance with got data

                db.Users.Add(newUser); // choice current object and add to the database
                db.SaveChanges(); // save new data in the database
                form.SendingData("command performed"); // call method SendingData for send current string

                var users = db.Users; // got all users from the database

                form.UsersEvent += form.ShowUsers; // defined current method 'ShowUsers' for event - 'UsersEvent'
                if (form.UsersEvent != null) // if event have method for performing
                {
                    form.UsersEvent(users); // call current event
                }
            }
        }
        private void ShowUsers(DbSet<User> usersAll) // method for showing all data about users from the database
        {
            Form1 helpVariable = new Form1(); // instance for calling current method -> 'SendingData'
            foreach (User u in usersAll) // loop operator
            {
                helpVariable.SendingData($"{u.Id}.{u.Name} - {u.Age}"); // set certain data about current user to the method - > 'SendingData'
            }
        }
        private static void DeleteUser() // method for deleting certain user in the database
        {
            Form1 ser2 = new Form1(); // create instance class ServerKernel

            ser2.SendingData("introduce User's Id"); // call method SendingData for send current string
            var userId = ser2.ReceivingData(); // call method ReceivingData for get all data from client part
            int userI = Int32.Parse(userId); // convert get clients data to the int type

            using (UserContext db = new UserContext()) // defined instance about class UserContext
            {
                var users = db.Users; // got all users from database
                foreach (User u in users) // get access about each user in the database
                {
                    if (u.Id == userI) // if id will searched in the database then
                    {
                        db.Users.Remove(u); // delete current user from database
                        break; // and will stop current increment
                    }
                }
                db.SaveChanges(); // save database after delete certain user
                ser2.SendingData("command performed"); // call method SendingData for send current string
                var usersAll = db.Users; // got data about all users from database
                ser2.SendingData("all Users from database"); // call method SendingData for send current string
                foreach (User u in usersAll) // will got access to the every user in the database
                {
                    ser2.SendingData($"{u.Id}.{u.Name} - {u.Age}"); // will call method SendingData for send certain data about current user
                }
            }
        }
        private static void ExitProgram() // method for exit from program
        {
            Environment.Exit(0);
        }

        private void ShowMarker(string data) // function for showing string data
        {
            outData.AppendText(data);
        }

    }
}
