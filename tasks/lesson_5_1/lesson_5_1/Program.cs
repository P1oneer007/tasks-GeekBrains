using System;
using System.IO;
class Program
{
    static void Main()
    {
        Console.WriteLine("Введите данные для сохранения в файл:");
        string data = Console.ReadLine();
        Console.WriteLine("Введите имя файла:");
        string fileName = Console.ReadLine();
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(data);
            }
            Console.WriteLine("Данные успешно сохранены в файле " + fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
        }
        Console.ReadLine();
    }
}