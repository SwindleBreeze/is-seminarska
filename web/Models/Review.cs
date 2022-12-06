using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public int EventID { get; set; }
        public string comment { get; set; }
        public int grade { get; set; }
        public Profile Profile { get; set; }
        public Event Event { get; set; }

    }
}
