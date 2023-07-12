using System;
class Program
{
    static void Main(string[] args)
    {
        //Двумерный массив
        int[,] array = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        // Вывод элементов диагонали
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == j)
                {
                    Console.Write(array[i, j] + " ");
                }
            }
        }
        Console.ReadLine();
    }
}