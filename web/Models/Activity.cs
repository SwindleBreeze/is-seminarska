using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models{
    public class Activity{
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(450)]
        public string Description { get; set; }
        public ICollection<Event_has_activity> Event_Has_Activities { get; set; }

    }
}