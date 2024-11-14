using Microsoft.EntityFrameworkCore;
using static EFCoreCRUD.Class;

namespace EFCoreCRUD
{
    class Program
    {
        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1 - Добавление клиента");
                Console.WriteLine("2 - Получение клиентов");
                Console.WriteLine("3 - Редактирование клиента");
                Console.WriteLine("4 - Удаление клиента");
                Console.WriteLine("0 - Выход");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddCustomer();
                        break;
                    case 2:
                        GetCustomers();
                        break;
                    case 3:
                        EditCustomer();
                        break;
                    case 4:
                        DeleteCustomer();
                        break;
                    case 0:
                        exit = true;
                        Console.WriteLine("Выход из программы.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        static void AddCustomer()
        {
            Console.Write("Введите ID клиента: ");
            string customerId = Console.ReadLine();

            Console.Write("Введите название компании: ");
            string companyName = Console.ReadLine();

            Console.Write("Введите имя контакта: ");
            string contactName = Console.ReadLine();

            using (NorthwindContext db = new NorthwindContext())
            {
                var newCustomer = new Customer { CustomerID = customerId, CompanyName = companyName, ContactName = contactName };
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                Console.WriteLine("Клиент добавлен:");
                Console.WriteLine($"{newCustomer.CustomerID} - {newCustomer.CompanyName} - {newCustomer.ContactName}");
            }
        }
        static void GetCustomers()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                var customers = db.Customers.ToList();
                Console.WriteLine("Данные клиентов:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");
                }
            }
        }
        static void EditCustomer()
        {
            Console.Write("Введите ID клиента, которого хотите отредактировать: ");
            string customerId = Console.ReadLine();

            using (NorthwindContext db = new NorthwindContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                if (customer != null)
                {
                    Console.WriteLine("Текущие данные клиента:");
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");

                    Console.Write("Введите новое CompanyName: ");
                    customer.CompanyName = Console.ReadLine();

                    Console.Write("Введите новое имя контакта: ");
                    customer.ContactName = Console.ReadLine();

                    db.SaveChanges();

                    Console.WriteLine("Данные клиента после редактирования:");
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");
                }
                else
                {
                    Console.WriteLine("Клиент не найден.");
                }
            }
        }
        static void DeleteCustomer()
        {
            Console.Write("Введите ID клиента, которого хотите удалить:");
            string customerId = Console.ReadLine();

            using (var db = new NorthwindContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                if (customer != null)
                {
                    var orderDetailsToDelete = db.OrderDetails.Where(od => od.Order.CustomerID == customerId);

                    db.OrderDetails.RemoveRange(orderDetailsToDelete);

                    var ordersToDelete = db.Orders.Where(o => o.CustomerID == customerId);

                    db.Orders.RemoveRange(ordersToDelete);

                    db.Customers.Remove(customer);

                    try
                    {
                        db.SaveChanges();
                        Console.WriteLine("Клиент и все связанные данные успешно удалены.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении изменений: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Клиент не найден.");
                }
            }
        }
    }
}
