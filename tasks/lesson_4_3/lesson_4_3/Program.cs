using System;
enum Season
{
    Winter,
    Spring,
    Summer,
    Autumn
}
class Program
{
    static Season GetSeason(int month)
    {
        switch (month)
        {
            case 1:
            case 2:
            case 12:
                return Season.Winter;
            case 3:
            case 4:
            case 5:
                return Season.Spring;
            case 6:
            case 7:
            case 8:
                return Season.Summer;
            case 9:
            case 10:
            case 11:
                return Season.Autumn;
            default:
                throw new ArgumentException("Ошибка: введите число от 1 до 12");
        }
    }
    static string GetSeasonName(Season season)
    {
        switch (season)
        {
            case Season.Winter:
                return "зима";
            case Season.Spring:
                return "весна";
            case Season.Summer:
                return "лето";
            case Season.Autumn:
                return "осень";
            default:
                throw new ArgumentException("Ошибка: некорректное значение времени года");
        }
    }
    static void Main(string[] args)
    {
        Console.Write("Введите номер месяца (от 1 до 12): ");
        if (int.TryParse(Console.ReadLine(), out int month))
        {
            try
            {
                Season season = GetSeason(month);
                string seasonName = GetSeasonName(season);
                Console.WriteLine($"Время года: {seasonName}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введите число");
        }  
    }
}