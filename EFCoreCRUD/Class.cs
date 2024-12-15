using Microsoft.EntityFrameworkCore;

namespace EFCoreCRUD
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } // Таблица клиентов
        public DbSet<Order> Orders { get; set; } // Таблица заказов
        public DbSet<OrderDetails> OrderDetails { get; set; } // Таблица деталей заказа
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Указание базы данных SQLite
            optionsBuilder.UseSqlite("DataSource=NorthwindSQLite.sqlite");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasKey(od => new { od.OrderID, od.ProductID });
        }
    }

    public class Customer
    {
        // Уникальный идентификатор клиента
        public string CustomerID { get; set; }

        // Название компании
        public string CompanyName { get; set; }

        // Имя контакта
        public string ContactName { get; set; }
    }
    public class Order
    {
        // Уникальный идентификатор заказа
        public int OrderID { get; set; }

        // ID клиента, связанного с заказом
        public string CustomerID { get; set; }

        // Связь с таблицей клиентов
        public Customer Customer { get; set; }
    }
    public class OrderDetails
    {
        // ID заказа
        public int OrderID { get; set; }

        // ID продукта
        public int ProductID { get; set; }

        // Связь с таблицей заказов
        public Order Order { get; set; }
    }
}
