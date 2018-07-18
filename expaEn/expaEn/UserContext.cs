using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace expaEn
{
    class UserContext : DbContext // defined current context data for use interaction with DB
    {
        public UserContext() : base("DbConnection")
        { }

        public DbSet<User> Users { get; set; } // defined all data in the DB
    }
}
