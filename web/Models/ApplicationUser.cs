using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ProfileID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? password { get; set; }
        public int? regionID { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime DoB { get; set; }
        public Region? region { get; set; }

        public ICollection<Profile_Has_Events> Profile_Has_Events { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}