using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data.Entity;

namespace ServerWithEntityFramework
{
    class StartPoint // class for defined sequence algorithms and defined several methods for working with database
    {
        public delegate void ShowAllUsers(DbSet<User> users); // delegate for description next event
        public event ShowAllUsers UsersEvent; // event for performing users from database

        static void Main(string[] args) // start point
        {
            ServerKernel servKer = new ServerKernel(); // create instance about ServerKernel class
            servKer.AddEvent += AddUser; // binding performed for event AddEvent
            servKer.DelEvent += DeleteUser; // binding performed for event DelEvent
            servKer.ExitEvent += ExitProgram; // binding performed for current event ExitEvent

            StartPoint show = new StartPoint();
            show.UsersEvent += show.ShowUsers;

            //calling in sequence all methods
            servKer.DefinedHostComputer(); // ddefined host for new machine
            servKer.DefinedAddress(); // created full address about Server part
            servKer.DefinedEndPoint(); // defined all data about Server part
            servKer.OpenSocket(); // create socket with all data about Server part
            servKer.NamingSocket(); // naming socket for next connecting to the Server part
        }

        private static void AddUser() // method for defined add new User to the database
        {
            ServerKernel ser = new ServerKernel(); // create instance about class ServerKernel

            ser.SendingData("introduce User's Name"); // call method SendingData for send current string
            var userName = ser.ReceivingData(); // call method Receiving for got data from client part
            string userN = userName.ToString(); // convert get data to the type string

            ser.SendingData("introduce User's Age"); // call method SendingData for send current string
            var userAge = ser.ReceivingData(); // call method ReceivingData for get all data from client part
            int userA = Int32.Parse(userAge); // convert get data in the int type

            using (UserContext db = new UserContext()) // defined variable about class UserContext
            {
                User newUser = new User { Name = userN, Age = userA }; // defined instance with got data

                db.Users.Add(newUser); // choice current object and add to the database
                db.SaveChanges(); // save new data in the database
                ser.SendingData("command performed"); // call method SendingData for send current string

                var users = db.Users; // got all users from the database

                StartPoint showUsers = new StartPoint(); // instance for description event about showing all users
                showUsers.UsersEvent += showUsers.ShowUsers; // defined current method 'ShowUsers' for event - 'UsersEvent'
                if(showUsers.UsersEvent != null) // if event have method for performing
                {
                    showUsers.UsersEvent(users); // call current event
                }
            }

        }

        private void ShowUsers(DbSet<User> usersAll) // method for showing all data about users from the database
        {
            ServerKernel helpVariable = new ServerKernel(); // instance for calling current method -> 'SendingData'
            foreach(User u in usersAll) // loop operator
            {
                helpVariable.SendingData($"{u.Id}.{u.Name} - {u.Age}"); // set certain data about current user to the method - > 'SendingData'
            }
        }

        private static void DeleteUser() // method for deleting certain user in the database
        {
            ServerKernel ser2 = new ServerKernel(); // create instance class ServerKernel

            ser2.SendingData("introduce User's Id"); // call method SendingData for send current string
            var userId = ser2.ReceivingData(); // call method ReceivingData for get all data from client part
            int userI = Int32.Parse(userId); // convert get clients data to the int type

            using (UserContext db = new UserContext()) // defined instance about class UserContext
            {
                var users = db.Users; // got all users from database
                foreach(User u in users) // get access about each user in the database
                {
                    if(u.Id == userI) // if id will searched in the database then
                    {
                        db.Users.Remove(u); // delete current user from database
                        break; // and will stop current increment
                    }
                }
                db.SaveChanges(); // save database after delete certain user
                ser2.SendingData("command performed"); // call method SendingData for send current string
                var usersAll = db.Users; // got data about all users from database
                ser2.SendingData("all Users from database"); // call method SendingData for send current string
                foreach(User u in usersAll) // will got access to the every user in the database
                {
                    ser2.SendingData($"{u.Id}.{u.Name} - {u.Age}"); // will call method SendingData for send certain data about current user
                }
            }
        }

        private static void ExitProgram() // method for exit from program
        {
            Environment.Exit(0);
        }
    }
}
