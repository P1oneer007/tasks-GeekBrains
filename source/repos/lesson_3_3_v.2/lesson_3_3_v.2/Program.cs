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
        int length = charArray.Length;
        char[] reversedArray = new char[length];
        for (int i =0 ; i < length; i++)
        {
            reversedArray[i] = charArray[length - 1 - i];
        }
        return new string(reversedArray);
    }
}