using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        Task1(); Console.WriteLine(new string('-', 30)); Task2(); Console.WriteLine(new string('-', 30)); Task3();
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void Task1()
    {
        string[] candidates = { "Анна", "Борис", "Олег", "Ирина" };

        var query = candidates
            .Where(name => name.Length == 4)
            .OrderBy(name => name);

        candidates[2] = "Игорь";

        Console.WriteLine("Результат отложенного запроса:");
        foreach (var name in query)
        {
            Console.WriteLine(name);
        }

        var immediateList = candidates
            .Where(name => name.Length == 4)
            .OrderBy(name => name)
            .ToList();

        candidates[0] = "Алёна";

        Console.WriteLine("\nРезультат немедленного запроса (ToList):");
        foreach (var name in immediateList)
        {
            Console.WriteLine(name);
        }
    }

    static void Task2()
    {
        string[] products = { "Laptop", "Phone", "TABLET", "Smartwatch" };

        bool allLongerThan3 = products.All(p => p.Length > 3);
        Console.WriteLine($"Все продукты длиннее 3 символов: {allLongerThan3}");

        bool anyStartsWithS = products.Any(p => p.StartsWith("S", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"Есть продукт на 'S' / 's': {anyStartsWithS}");

        bool standardContains = products.Contains("phone");
        Console.WriteLine($"Стандартный Contains(\"phone\"): {standardContains}");

        bool customContains = products.Contains("phone", new IgnoreCaseComparer());
        Console.WriteLine($"Кастомный Contains(\"phone\"): {customContains}");
    }

    static void Task3()
    {
        User[] users = {
        new User("Tom", 20),
        new User("Bob", 25),
        new User("Sam", 30),
        new User("Alice", 22)
    };

        User? firstOlderThan21 = users.First(u => u.Age > 21);
        Console.WriteLine($"Первый старше 21: {firstOlderThan21.Name} ({firstOlderThan21.Age})");

        User? olderThan50 = users.FirstOrDefault(u => u.Age > 50);
        Console.WriteLine($"Найден старше 50: {(olderThan50 == null ? "null" : olderThan50.Name)}");

        // Безопасный кастомный дефолт через оператор ??
        User defaultUser = users.FirstOrDefault(u => u.Age > 50) ?? new User("Guest", 0);
        Console.WriteLine($"С кастомным дефолтом: {defaultUser.Name}, age: {defaultUser.Age}");

        User? last3Letter = users.LastOrDefault(u => u.Name.Length == 3);
        Console.WriteLine($"Последний с именем из 3 букв: {(last3Letter != null ? last3Letter.Name : "не найден")}");
    }
}
class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
class IgnoreCaseComparer : IEqualityComparer<string?>
{
    public bool Equals(string? x, string? y) { if (x == null && y == null) return true; if (x == null || y == null) return false; return x.Equals(y, StringComparison.OrdinalIgnoreCase); }
    public int GetHashCode(string? obj)
    {
        if (obj == null) return 0;
        return obj.ToLower().GetHashCode();
    }
}