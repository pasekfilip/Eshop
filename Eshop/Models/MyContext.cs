using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Eshop.Models.Tables;
namespace Eshop.Models
{
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public sealed class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductForOrder> Product_For_Orders { get; set; }

        private static MyContext _instance;
        private MyContext() { }
        public static MyContext GetInstance()
        {
            return _instance == null ? _instance = new MyContext() : _instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}