using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerWithEntityFramework
{
    class StartPoint // class for defined sequence algorithms and defined several methods for working with database
    {
        static void Main(string[] args) // start point
        {
            ServerKernel servKer = new ServerKernel(); // create instance about ServerKernel class
            servKer.AddEvent += AddUser; // binding performed for event AddEvent
            servKer.DelEvent += DeleteUser; // binding performed for event DelEvent
            servKer.ExitEvent += ExitProgram; // binding performed for current event ExitEvent

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
                ser.SendingData("list all users from database ->>"); // call method SendingData for send current string to the client part
                foreach(User u in users) // got access to the every object(user) in the database
                {
                    ser.SendingData($"{u.Id}.{u.Name} - {u.Age}"); // call method SendingData for send got data from database to the client part
                }
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
