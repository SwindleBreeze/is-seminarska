using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data{
    public class sloveniatrips : IdentityDbContext<ApplicationUser>
    {
        public sloveniatrips(DbContextOptions<sloveniatrips> options) : base(options)
        {
        }

        public DbSet<web.Models.Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ApplicationUser> Profiles { get; set; }
        public DbSet<Review> Reviews { get;set; }
        public DbSet<Profile_Has_Events> Profile_Has_Events { get;set; }
        public DbSet<Event_has_activity> Event_has_activities { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<web.Models.Activity>().ToTable("Activity");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<ApplicationUser>().ToTable("Profile");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<Profile_Has_Events>().ToTable("Profile_Has_Events");
            modelBuilder.Entity<Event_has_activity>().ToTable("Event_Has_activity");
        }
    }
}