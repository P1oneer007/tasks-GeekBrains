using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите набор чисел, разделенных пробелом:");
        string input = Console.ReadLine();
        string[] numbers = input.Split(' ');
        int sum =0;
        foreach (string number in numbers)
        {
            sum += int.Parse(number);
        }
        Console.WriteLine("Сумма чисел: " + sum);
    }
}