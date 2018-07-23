using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWithEntityFramework
{
    class User // class for description all data from database
    {
        public int Id { get; set; } // user's id
        public string Name { get; set; } // user's Name
        public int Age { get; set; } // user's Age
    }
}
