using Microsoft.EntityFrameworkCore;

namespace EFCoreCRUD
{
    class Program
    {
        static void Main()
        {
            // Переменная для выхода из цикла
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

                switch (choice) // Выбор операции
                {
                    case 1:
                        AddCustomer(); // Добавление нового клиента
                        break;
                    case 2:
                        GetCustomers(); // Получение списка клиентов
                        break;
                    case 3:
                        EditCustomer(); // Редактирование данных клиента
                        break;
                    case 4:
                        DeleteCustomer(); // Удаление клиента и связанных данных
                        break;
                    case 0:
                        exit = true; // Завершение программы
                        Console.WriteLine("Выход из программы.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова."); // Если пользователь ввел неверное число
                        break;
                }
            }
        }
        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        static void AddCustomer()
        {
            // Считываем данные нового клиента
            Console.Write("Введите ID клиента: ");
            string customerId = Console.ReadLine();

            Console.Write("Введите название компании: ");
            string companyName = Console.ReadLine();

            Console.Write("Введите имя контакта: ");
            string contactName = Console.ReadLine();

            // Создаем подключение к базе данных
            using (NorthwindContext db = new NorthwindContext())
            {
                // Создаем объект клиента и добавляем его в базу
                var newCustomer = new Customer { CustomerID = customerId, CompanyName = companyName, ContactName = contactName };
                db.Customers.Add(newCustomer);
                db.SaveChanges(); // Сохраняем изменения в базе
                Console.WriteLine("Клиент добавлен:");
                Console.WriteLine($"{newCustomer.CustomerID} - {newCustomer.CompanyName} - {newCustomer.ContactName}");
            }
        }
        /// <summary>
        /// Получение списка клиентов
        /// </summary>
        static void GetCustomers()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                var customers = db.Customers.ToList(); // Загружаем всех клиентов из базы
                Console.WriteLine("Данные клиентов:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");
                }
            }
        }
        /// <summary>
        /// Редактирование данных клиента
        /// </summary>
        static void EditCustomer()
        {
            Console.Write("Введите ID клиента, которого хотите отредактировать: ");
            string customerId = Console.ReadLine(); // Считываем ID клиента

            using (NorthwindContext db = new NorthwindContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId); // Ищем клиента по ID
                if (customer != null)
                {
                    Console.WriteLine("Текущие данные клиента:");
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");

                    Console.Write("Введите новое CompanyName: ");
                    customer.CompanyName = Console.ReadLine(); // Новое название компании

                    Console.Write("Введите новое имя контакта: ");
                    customer.ContactName = Console.ReadLine(); // Новое имя контакта

                    db.SaveChanges(); // Сохраняем изменения

                    Console.WriteLine("Данные клиента после редактирования:");
                    Console.WriteLine($"{customer.CustomerID} - {customer.CompanyName} - {customer.ContactName}");
                }
                else
                {
                    Console.WriteLine("Клиент не найден."); // Если клиент не найден
                }
            }
        }
        /// <summary>
        /// Удаление клиента и связанных данных
        /// </summary>
        static void DeleteCustomer()
        {
            // Считываем ID клиента, которого нужно удалить
            Console.Write("Введите ID клиента, которого хотите удалить:");
            string customerId = Console.ReadLine();

            using (var db = new NorthwindContext())
            {
                // Ищем клиента в базе данных
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                if (customer != null)
                {
                    // Находим и удаляем все связанные записи OrderDetails
                    var orderDetailsToDelete = db.OrderDetails.Where(od => od.Order.CustomerID == customerId);
                    db.OrderDetails.RemoveRange(orderDetailsToDelete);

                    // Находим и удаляем все заказы клиента
                    var ordersToDelete = db.Orders.Where(o => o.CustomerID == customerId);
                    db.Orders.RemoveRange(ordersToDelete);

                    // Удаляем самого клиента
                    db.Customers.Remove(customer);

                    try
                    {
                        db.SaveChanges();// Сохраняем изменения в базе
                        Console.WriteLine("Клиент и все связанные данные успешно удалены.");
                    }
                    catch (Exception ex)
                    {
                        // Если возникает ошибка при сохранении, выводим сообщение об ошибке
                        Console.WriteLine($"Ошибка при сохранении изменений: {ex.Message}");
                    }
                }
                else
                {
                    // Если клиент с указанным ID не найден
                    Console.WriteLine("Клиент не найден.");
                }
            }
        }
    }
}
