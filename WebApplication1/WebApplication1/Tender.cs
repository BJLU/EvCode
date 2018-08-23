using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Tender
    {
        public int Id { get; set; }
        public string Subject_tender { get; set; }
        public string Description_tender { get; set; }
        public string Organizer_tender { get; set; }
        public string Kind_tender { get; set; }
        public string Category_tender { get; set; }
        public int Budget_tender { get; set; }
        public string Currency_tender { get; set; }
        public DateTime Data_tender { get; set; }
        public DateTime AcceptFromThe { get; set; }
        public DateTime AcceptTo { get; set; }

    }
}