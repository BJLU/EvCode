using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace tst.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db) // method for add two user to the database
        {
            db.Users.Add(new User { Name = "Buda", Age = 22 }); // first user
            db.Users.Add(new User { Name = "Pesht", Age = 22 }); // second user

            base.Seed(db); 
        }
    }
}