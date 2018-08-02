using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tst.Models;

namespace tst.Controllers
{
    public class HomeController : Controller
    {
        UserContext db = new UserContext(); // variable for get access to the database EntityFramework

        public ActionResult Index() // call base View for watching all users from the database
        {
            var users = db.Users; // get all users to the current variable
            ViewBag.Users = users; // perform all users for next View
            return View(); // call View about Index method
        }

        [HttpGet] // method for get query
        public ActionResult Add() // method for call View about add new user
        {
            return View(); // call View for introduce new data
        }

        [HttpPost] // method for post query
        public string Add(string name, int age) // method for add new user to the database
        {
            User userNew = new User { Name = name, Age = age }; // variable for user's data

            db.Users.Add(userNew); // add new user to the database
            db.SaveChanges(); // save new user in the database
            
            return "user added"; // output current string
        }

        [HttpGet] // method for get query
        public ActionResult Del() // method for call view about current function
        {
            return View(); // call view about current method
        }

        [HttpPost] // method for post query
        public string Del(int Id) // method for deleting user by id
        {
            var users = db.Users; // get all users from the database
            foreach(var ty in users) // loop operator for get access to the every users from the database
            {
                if(ty.Id == Id) // if found certain user's Id, then
                {
                    db.Users.Remove(ty); // delete current user from the database
                    db.SaveChanges(); // save database without deleted
                    break; // will interrupt loop operator
                }
            }

            return "user deleted"; // output current string
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}