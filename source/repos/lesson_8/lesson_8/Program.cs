using System;
using System.Configuration;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Выводим приветствие из настроек приложения
            string greeting = ConfigurationManager.AppSettings["Greeting"];
            Console.WriteLine(greeting);

            // Запрашиваем у пользователя данные
            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите Ваш возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите Вашу род деятельности: ");
            string occupation = Console.ReadLine();

            // Сохраняем данные в настройках
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Name"].Value = name;
            config.AppSettings.Settings["Age"].Value = age.ToString();
            config.AppSettings.Settings["Occupation"].Value = occupation;
           // config.Save(ConfigurationSaveMode.Modified);

            Console.WriteLine("Данные сохранены.");

            // Выводим данные при следующем запуске
            Console.WriteLine("Имя: " + config.AppSettings.Settings["Name"].Value);
            Console.WriteLine("Возраст: " + config.AppSettings.Settings["Age"].Value);
            Console.WriteLine("Род деятельности: " + config.AppSettings.Settings["Occupation"].Value);

            Console.ReadLine();
        }
    }
}