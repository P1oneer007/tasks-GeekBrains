using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите строку:");
        string input = Console.ReadLine();

        string reversed = ReverseString(input);
        Console.WriteLine("Результат: " + reversed);

        Console.ReadLine();
    }
    static string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}