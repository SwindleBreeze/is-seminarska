using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Profile_Has_Events
    {
        public int ID { get; set; }
        public ApplicationUser Profile { get; set; }
        public Event Event { get; set; }
    }
}