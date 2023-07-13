using System;
class Program
{
    static void Main(string[] args)
    {
        // Предварительно заготовленные данные
        string date = "10.07.2022";
        string storeName = "Магазин \"Пятёрочка\"";
        double totalAmount = 578.2;

        // Вывод чека в консоль
        Console.WriteLine("----------------------------");
        Console.WriteLine("         Чек покупок");
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Дата: {date}");
        Console.WriteLine($"Магазин: {storeName}");
        Console.WriteLine("----------------------------");
        Console.WriteLine("Товары:");
        Console.WriteLine("1. Хлеб    - 25.00");
        Console.WriteLine("2. Масло   - 157.50");
        Console.WriteLine("3. Сыр     - 70.75");
        Console.WriteLine("3. Колбаса - 325.45");
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Общая сумма: {totalAmount}");
        Console.WriteLine("----------------------------");
    }
}