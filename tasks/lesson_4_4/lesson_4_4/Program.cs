using System;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите номер числа Фибоначчи: ");
        int n = int.Parse(Console.ReadLine());
        int result = Fibonacci(n);
        Console.WriteLine($"Число Фибоначчи для {n}-го элемента: {result}");
    }
    static int Fibonacci(int n)
    {
        if (n <= 1)
           return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
}