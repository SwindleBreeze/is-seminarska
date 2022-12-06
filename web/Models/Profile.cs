using System;
using System.Collections.Generic;
namespace web.Models
{
    public class Profile
    {
        public int ID{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int? regionID { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime DoB { get; set; }
        public Region? region { get; set; }

        public ICollection<Profile_Has_Events> Profile_Has_Events { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}