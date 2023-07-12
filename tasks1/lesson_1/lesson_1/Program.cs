using System;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var UserName = Environment.UserName;
            Console.WriteLine($"Привет, {UserName}, сегодня {System.DateTime.Now:dd.MM.yyyy}");
        }
    }
}