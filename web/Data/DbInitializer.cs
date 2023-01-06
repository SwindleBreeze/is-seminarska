using web.Data;
using System;
using System.Linq;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(sloveniatrips context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}