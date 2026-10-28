using System;
using System.IO;
using System.Data.SQLite;
public static class DatabaseInitializer
{
    private const string DbFile = "enterprise.db"; private static readonly string ConnectionString = $"Data Source={DbFile}";
    public static void Initialize()
    {
        if (File.Exists(DbFile)) File.Delete(DbFile);

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE Categories (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, DiscountRate REAL NOT NULL);
        CREATE TABLE Products (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, CategoryId INTEGER NOT NULL, Price REAL NOT NULL, Stock INTEGER NOT NULL);
        CREATE TABLE Sales (Id INTEGER PRIMARY KEY AUTOINCREMENT, ProductId INTEGER NOT NULL, CustomerName TEXT NOT NULL, Quantity INTEGER NOT NULL, SaleDate TEXT NOT NULL, Channel TEXT NOT NULL);

        INSERT INTO Categories (Name, DiscountRate) VALUES ('Электроника', 0.10), ('Одежда', 0.05), ('Книги', 0.15), ('Бытовая техника', 0.20);
        INSERT INTO Products (Name, CategoryId, Price, Stock) VALUES ('Ноутбук', 1, 450000, 15), ('Смартфон', 1, 250000, 30), ('Наушники', 1, 35000, 50), ('Куртка', 2, 45000, 20), ('Джинсы', 2, 25000, 40);
        INSERT INTO Sales (ProductId, CustomerName, Quantity, SaleDate, Channel) VALUES (1, 'Иванов И.И.', 1, '2026-01-10', 'Online'), (2, 'Петров П.П.', 2, '2026-01-11', 'Retail'), (3, 'Сидоров С.С.', 1, '2026-01-12', 'Online');
    ";
        command.ExecuteNonQuery();
    }
}