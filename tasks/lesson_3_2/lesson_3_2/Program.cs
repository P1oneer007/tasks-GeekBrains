using System;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
   
        string[,] phoneBook = new string[5, 2];

        phoneBook[0,0] = "Иванов";
        phoneBook[0, 1] = "89265876950";
        phoneBook[1,0 ] = "Петров";
        phoneBook[1, 1] = "89832662519";
        phoneBook[2,0 ] = "Сидоров";
        phoneBook[2, 1] = "89281971416";
        phoneBook[3, 0] = "Смирнов";
        phoneBook[3, 1] = "89242395674";
        phoneBook[4, 0] = "Кузнецов";
        phoneBook[4, 1] = "89265411193";

        Console.WriteLine($"Контакт:  Номер: ");
        for (int i = 0; i < 5; i++)
        {
            string name = phoneBook[i,0];
            string number = phoneBook[i, 1];
            Console.WriteLine($"{name}, {number}");
        }

        Console.ReadLine();
    }
}