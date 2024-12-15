# EFCoreCRUD

## Project Description
This console application interacts with a Northwind database implemented in SQLite. 
It provides basic functionality for managing customers 
and their data, including adding, retrieving, editing, and deleting customers. 
Associated orders and order details are also correctly handled during deletion.

## Features
The application supports the following operations:

1. Add a Customer: Create a new customer with a unique ID, company name, and contact name.

2. Retrieve Customers: Display all customers from the database.

3. Edit a Customer: Update the details of an existing customer.

4. Delete a Customer: Remove a customer and all associated orders and order details.

5. Exit the Program: End the application's execution.

## Technologies Used
- C# for application logic.

- Entity Framework Core for database interaction.

- SQLite for data storage.

## Project Structure
### Classes
1. NorthwindContext:

- Database context for Entity Framework.
- Tables: Customers, Orders, OrderDetails.
- Configures a composite key for the OrderDetails table.

2. Customer:
- Stores customer information (ID, company name, contact name).

3. Order:
- Stores order information (Order ID, Customer ID).
- Linked to customers via a foreign key.

4. OrderDetails:
- Stores order details (Order ID, Product ID).
- Uses a composite key (OrderID and ProductID).

5. Program:

- Contains the main interface and methods to perform database operations.

## Methods
- AddCustomer(): Adds a new customer to the database.

- GetCustomers(): Displays a list of all customers.

- EditCustomer(): Updates a customer's details by their ID.

- DeleteCustomer(): Deletes a customer and associated data (orders and order details).

- Main(): Handles user input and invokes the corresponding methods.

## Project links
[Основные операции с данными. CRUD](https://metanit.com/sharp/efcore/1.4.php)
[Руководство по Entity Framework Core 9](https://metanit.com/sharp/efcore/)