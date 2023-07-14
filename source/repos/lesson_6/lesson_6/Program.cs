using System;
using System.Diagnostics;
namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Вывести список запущенных процессов");
                Console.WriteLine("2. Завершить процесс по ID");
                Console.WriteLine("3. Завершить процесс по имени");
                Console.WriteLine("4. Выйти");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PrintRunningProcesses();
                        break;
                    case "2":
                        TerminateProcessById();
                        break;
                    case "3":
                        TerminateProcessByName();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
                Console.WriteLine();
            }
        }
        static void PrintRunningProcesses()
        {
            Console.WriteLine("Список запущенных процессов:");
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                Console.WriteLine($"ID: {process.Id}, Имя: {process.ProcessName}");
            }
        }
        static void TerminateProcessById()
        {
            Console.Write("Введите ID процесса для завершения: ");
            int processId;
            if (int.TryParse(Console.ReadLine(), out processId))
            {
                try
                {
                    Process process = Process.GetProcessById(processId);
                    process.Kill();
                    Console.WriteLine("Процесс успешно завершен.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Процесс с указанным ID не найден.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при завершении процесса: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID процесса.");
            }
        }
        static void TerminateProcessByName()
        {
            Console.Write("Введите имя процесса для завершения: ");
            string processName = Console.ReadLine();
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                    Console.WriteLine("Процесс успешно завершен.");
                }
                else
                {
                    Console.WriteLine("Процесс с указанным именем не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при завершении процесса: {ex.Message}");
            }
        }
    }
}