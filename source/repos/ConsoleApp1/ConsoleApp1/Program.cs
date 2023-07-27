using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ReadSetting("Greetings") != "")
            {
                Console.WriteLine($"{ReadSetting("Greetings")}");
            }
            else
            {
                AddUpdateAppSettings("Greetings", "Приветствую тебя, дикий пользователь!");
            }

            if (ReadSetting("UserName") != "")
            {
                Console.WriteLine($"Имя пользователя: {ReadSetting("UserName")}");
            }
            else
            {
                Console.Write("Введите имя пользователя: ");
                string userName = Console.ReadLine();
                AddUpdateAppSettings("UserName", userName);
            }
            if (ReadSetting("Age") != "")
            {
                Console.WriteLine($"Возраст пользователя: {ReadSetting("Age")}");
            }
            else
            {
                Console.Write("Введите возраст пользователя: ");
                string age = Console.ReadLine();
                AddUpdateAppSettings("Age", age);
            }
            if (ReadSetting("Job") != "")
            {
                Console.WriteLine($"Род деятельности пользователя: {ReadSetting("Job")}");
            }
            else
            {
                Console.Write("Введите род деятельности пользователя: ");
                string job = Console.ReadLine();
                AddUpdateAppSettings("Job", job);
            }
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                //;
            }
        }
    }
}