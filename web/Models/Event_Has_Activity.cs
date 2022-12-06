using System;
using System.Collections.Generic;

namespace web.Models{
    public class Event_has_activity{
        public int ID { get; set; }

        public int ActivityID { get; set; }
        public int regionID { get; set; }
        public Event Event { get; set; }
        public Activity Activity { get; set; }
    }
}