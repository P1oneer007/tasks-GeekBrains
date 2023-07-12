using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите минимальную температуру за сутки:");
        double minTemperature = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Введите максимальную температуру за сутки:");
        double maxTemperature = Convert.ToDouble(Console.ReadLine());

        double averageTemperature = (minTemperature + maxTemperature) / 2;

        Console.WriteLine("Среднесуточная температура: " + averageTemperature);
    }
}