using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expaEn
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Program Commands");
            Console.WriteLine("add - add new user");
            Console.WriteLine("del - delete certain");
            Console.WriteLine("ex - exit");
            while(true) // for use all functions without restrictions
            {
                CommandsRezerv(); // method for recognition commands
            }
        }

        private static void CommandsRezerv() // method for processing all commands
        {
            var command = Console.ReadLine(); // variable for input data

            if (command == "add") // if command add user
            {
                Console.WriteLine("com -> add");
                AddUser(); // call method for add new user to the db
            }
            else if(command == "del") // if command delete user
            {
                Console.WriteLine("com -> del");
                DelUser(); // call method for delete user
            }
            else if(command == "ex") // if command exit
            {
                Console.WriteLine("com -> exit");
                Environment.Exit(0); // close application
            }
        }

        private static void AddUser() // method for introduce new user to the data base
        {
            Console.WriteLine("introduce your name -> ");
            var uName = Console.ReadLine(); // variable for input data about user's Name
            Console.WriteLine("Introduce your age -> ");
            var uAge = Console.ReadLine(); // variable for input data about user's Age
            int iAge = Int32.Parse(uAge); // conversion current type of the - 'uAge' to the type - 'int'
            

            using (UserContext db = new UserContext()) // open access to the DB through variable - 'db'
            {
                User currentUser = new User { Name = uName, Age = iAge }; // transmit data about new user in the property class' User

                db.Users.Add(currentUser); // call method Add() through Users, and introduce new user to the DB
                db.SaveChanges(); // call method SaveChanges() for saved changes, saved new user in the DB
                Console.WriteLine("data saved");

                var users = db.Users; // get all data about users from the DB
                Console.WriteLine("list of the users: ");
                foreach(User u in users) // loop operator for output data about every user
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            Console.WriteLine("Command executed add\\"); // reference point about ended perform command
        }

        private static void DelUser() // method for delete user through certain id
        {
            Console.WriteLine("find users through id ->");
            var uId = Console.ReadLine(); // variable for chosen number id
            int intId = Int32.Parse(uId); // conversion from implicit type - 'uId'- to the type - 'int'

            using (UserContext db = new UserContext()) // get access to the DB through variable - 'db'
            {
                var users = db.Users; // get all users from the DB
                foreach(User u in users) // loop operator for output data about every user
                {
                    if(u.Id == intId) // if searched certain user's id ended succeeded
                    {
                        db.Users.Remove(u); // call method Remove() through Users
                        break; // and stopped loop operator
                    }
                }

                db.SaveChanges(); // save all changes in the DB after delete certain user
                Console.WriteLine("changes saved");

                var listUsers = db.Users; // get all data about users from DB
                Console.WriteLine("list of the users: ");
                foreach (var user in listUsers) // loop operator for output data about every user from the DB
                {
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                }
            }

            Console.WriteLine("command executed del\\"); // signal's string about ended perform command
        }
    }
}
