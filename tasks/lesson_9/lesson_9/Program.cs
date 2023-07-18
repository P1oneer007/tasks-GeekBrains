using System;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer
{
    class Program
    {
        static string currentDirectory;
        static int itemsPerPage;
        static List<string> commandHistory;
        static int commandIndex;

        static void Main(string[] args)
        {
            currentDirectory = Directory.GetCurrentDirectory();
            itemsPerPage = ReadItemsPerPageFromConfig();
            commandHistory = new List<string>();
            commandIndex = -1;

            while (true)
            {
                Console.Clear();
                PrintCurrentDirectory();
                PrintFileStructure();

                Console.WriteLine("\nAvailable commands: ");
                Console.WriteLine("1. cd <directory> - Change directory");
                Console.WriteLine("2. copy <source> <destination> - Copy file or directory");
                Console.WriteLine("3. delete <path> - Delete file or directory");
                Console.WriteLine("4. info <path> - Get file or directory information");
                Console.WriteLine("5. exit - Exit the file manager");

                Console.Write("\nEnter a command: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    commandHistory.Add(input);
                    commandIndex = commandHistory.Count - 1;

                    string[] commandParts = input.Split(' ');
                    string command = commandParts[0].ToLower();

                    try
                    {
                        switch (command)
                        {
                            case "cd":
                                if (commandParts.Length < 2)
                                    throw new ArgumentException("Invalid command format. Usage: cd <directory>");

                                string newDirectory = Path.Combine(currentDirectory, commandParts[1]);
                                if (!Directory.Exists(newDirectory))
                                    throw new DirectoryNotFoundException($"Directory '{newDirectory}' not found.");

                                currentDirectory = newDirectory;
                                break;

                            case "copy":
                                if (commandParts.Length < 3)
                                    throw new ArgumentException("Invalid command format. Usage: copy <source> <destination>");

                                string sourcePath = Path.Combine(currentDirectory, commandParts[1]);
                                string destinationPath = Path.Combine(currentDirectory, commandParts[2]);

                                if (File.Exists(sourcePath))
                                {
                                    File.Copy(sourcePath, destinationPath);
                                    Console.WriteLine("File copied successfully.");
                                }
                                else if (Directory.Exists(sourcePath))
                                {
                                    CopyDirectory(sourcePath, destinationPath);
                                    Console.WriteLine("Directory copied successfully.");
                                }
                                else
                                {
                                    throw new FileNotFoundException($"File or directory '{sourcePath}' not found.");
                                }
                                break;

                            case "delete":
                                if (commandParts.Length < 2)
                                    throw new ArgumentException("Invalid command format. Usage: delete <path>");

                                string pathToDelete = Path.Combine(currentDirectory, commandParts[1]);

                                if (File.Exists(pathToDelete))
                                {
                                    File.Delete(pathToDelete);
                                    Console.WriteLine("File deleted successfully.");
                                }
                                else if (Directory.Exists(pathToDelete))
                                {
                                    Directory.Delete(pathToDelete, true);
                                    Console.WriteLine("Directory deleted successfully.");
                                }
                                else
                                {
                                    throw new FileNotFoundException($"File or directory '{pathToDelete}' not found.");
                                }
                                break;

                            case "info":
                                if (commandParts.Length < 2)
                                    throw new ArgumentException("Invalid command format. Usage: info <path>");

                                string pathToInfo = Path.Combine(currentDirectory, commandParts[1]);

                                if (File.Exists(pathToInfo))
                                {
                                    FileInfo fileInfo = new FileInfo(pathToInfo);
                                    Console.WriteLine($"Name: {fileInfo.Name}");
                                    Console.WriteLine($"Size: {fileInfo.Length} bytes");
                                    Console.WriteLine($"Attributes: {fileInfo.Attributes}");
                                }
                                else if (Directory.Exists(pathToInfo))
                                {
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathToInfo);
                                    Console.WriteLine($"Name: {directoryInfo.Name}");
                                    Console.WriteLine($"Attributes: {directoryInfo.Attributes}");
                                }
                                else
                                {
                                    throw new FileNotFoundException($"File or directory '{pathToInfo}' not found.");
                                }
                                break;

                            case "exit":
                                SaveCommandHistory();
                                return;

                            default:
                                Console.WriteLine("Invalid command.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        LogException(ex);
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void PrintCurrentDirectory()
        {
            Console.WriteLine($"Current Directory: {currentDirectory}");
        }

        static void PrintFileStructure()
        {
            string[] directories = Directory.GetDirectories(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory);

            int totalItems = directories.Length + files.Length;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            Console.WriteLine("\nFile Structure:");
            Console.WriteLine($"Page 1 of {totalPages}");

            int currentPage = 1;
            int currentItem = 1;

            foreach (string directory in directories)
            {
                Console.WriteLine($"[{currentItem}] {Path.GetFileName(directory)}");
                currentItem++;

                if (currentItem > itemsPerPage)
                {
                    currentPage++;
                    if (currentPage <= totalPages)
                    {
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        PrintCurrentDirectory();
                        Console.WriteLine("\nFile Structure:");
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        currentItem = 1;
                    }
                }
            }

            foreach (string file in files)
            {
                Console.WriteLine($"[{currentItem}] {Path.GetFileName(file)}");
                currentItem++;

                if (currentItem > itemsPerPage)
                {
                    currentPage++;
                    if (currentPage <= totalPages)
                    {
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        PrintCurrentDirectory();
                        Console.WriteLine("\nFile Structure:");
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        currentItem = 1;
                    }
                }
            }
        }

        static int ReadItemsPerPageFromConfig()
        {

            return 10;
        }

        static void CopyDirectory(string source, string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(file);
                string destinationFile = Path.Combine(destination, fileName);
                File.Copy(file, destinationFile);
            }

            foreach (string subdirectory in Directory.GetDirectories(source))
            {
                string directoryName = Path.GetFileName(subdirectory);
                string destinationSubdirectory = Path.Combine(destination, directoryName);
                CopyDirectory(subdirectory, destinationSubdirectory);
            }
        }

        static void SaveCommandHistory()
        {

        }

        static void LogException(Exception ex)
        {
            string errorLogDirectory = Path.Combine(currentDirectory, "errors");
            Directory.CreateDirectory(errorLogDirectory);

            string errorLogFile = Path.Combine(errorLogDirectory, $"{Guid.NewGuid()}_exception.txt");
            File.WriteAllText(errorLogFile, ex.ToString());
        }
    }
}