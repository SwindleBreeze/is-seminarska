using System;
using System.Collections.Generic;

namespace web.Models{
    public class Event{
        public int ID { get; set; }
        public string Name { get; set; }
        public int regionID { get; set; }
        public DateTime Date { get; set; }
        public Region region { get; set; }
        public ICollection<Event_has_activity> Event_has_activities { get; set; }
        public ICollection<Profile_Has_Events> Profile_Has_Events { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Location { get; set; }

    }
}