using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string comment { get; set; }
        public int grade { get; set; }
        public ApplicationUser Profile { get; set; }
        public Event Event { get; set; }

    }
}
