using System;
using System.IO;
class Program
{
    static void Main()
    {
        Console.WriteLine("Введите числа (0...255) через пробел:");
        string input = Console.ReadLine();
        string[] numbers = input.Split(' ');
        byte[] bytes = new byte[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            if (byte.TryParse(numbers[i], out byte value))
            {
                bytes[i] = value;
            }
            else
            {
                Console.WriteLine($"Ошибка: '{numbers[i]}' не является допустимым числом (...255).");
                return;
            }
        }
        try
        {
            using (FileStream fileStream = new FileStream("numbers.bin", FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
            Console.WriteLine("Числа успешно записаны в файл 'numbers.bin'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
}