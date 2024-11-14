using Microsoft.EntityFrameworkCore;

namespace EFCoreCRUD
{
    internal class Class
    {
        public class NorthwindContext : DbContext
        {
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetails> OrderDetails { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("DataSource=NorthwindSQLite.sqlite");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<OrderDetails>().HasKey(od => new { od.OrderID, od.ProductID });
            }
        }

        public class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
        }
        public class Order
        {
            public int OrderID { get; set; }
            public string CustomerID { get; set; }
            public Customer Customer { get; set; }
        }
        public class OrderDetails
        {
            public int OrderID { get; set; }
            public int ProductID { get; set; }
            public Order Order { get; set; }
        }
    }
}
