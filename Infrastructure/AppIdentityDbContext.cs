using CrmCoreLite.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("CrmCore") {
        }

         
        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());

        }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
        //public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
        //{
        //    protected override void Seed(AppIdentityDbContext context)
        //    {
        //        PerformInitialSetup(context);
        //        base.Seed(context);
        //    }
        //    public void PerformInitialSetup(AppIdentityDbContext context)
        //    {
        //        // initial configuration will go here
        //    }
        //}
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}
