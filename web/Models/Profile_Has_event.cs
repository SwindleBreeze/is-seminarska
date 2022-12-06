using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Profile_Has_Events
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public Profile Profile { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}