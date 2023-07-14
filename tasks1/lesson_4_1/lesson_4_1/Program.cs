using System;
class Program
{
    static string GetFullName(string firstName, string lastName, string patronymic)
    {
        return $"{lastName} {firstName} {patronymic}";
    }

    static void Main(string[] args)
    {
        Console.WriteLine(GetFullName("Иван", "Иванов", "Иванович"));
        Console.WriteLine(GetFullName("Алексей", "Петров", "Алексеевич"));
        Console.WriteLine(GetFullName("Мария", "Сидорова", "Андреевна"));
        Console.WriteLine(GetFullName("Елена", "Козлова", "Сергеевна"));
    }
}