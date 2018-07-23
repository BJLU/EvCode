using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServerWithEntityFramework
{
    class UserContext : DbContext // class for communication with database -> 'EntityFramework'
    {
        public UserContext()
            :base("DbConnection")
        { }

        public DbSet<User> Users { get; set; } // entity for working data from database
    }
}
