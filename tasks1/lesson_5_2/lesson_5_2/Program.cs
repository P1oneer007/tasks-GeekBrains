using System;
using System.IO;
class Program
{
    static void Main()
    {
        string fileName = "startup.txt";
        string currentTime = DateTime.Now.ToString();
        try
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(currentTime);
            }
            Console.WriteLine("Текущее время успешно записано в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при записи в файл: " + ex.Message);
        }
    }
}