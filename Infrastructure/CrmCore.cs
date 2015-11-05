using CrmCoreLite.Areas.Brokers.Models;
using CrmCoreLite.Areas.Billings.Models;
using CrmCoreLite.Areas.Customers.Models;
using CrmCoreLite.Areas.Units.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrmCoreLite.Models;
using CrmCoreLite.Areas.Orders.Models;
using CrmCoreLite.Areas.Products.Models;

namespace CrmCoreLite.Infrastructure
{
    public class CrmCore : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Insentive> Insentives { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Basement> Basements { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Customer>().MapToStoredProcedures();
        }

    }
}
