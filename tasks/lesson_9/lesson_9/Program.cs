﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
            Console.SetWindowSize(80, 30);
            Console.Title = "File Explorer";
            currentDirectory = Directory.GetCurrentDirectory();
            itemsPerPage = ReadItemsPerPageFromConfig();
            commandHistory = new List<string>();
            commandIndex = -1;

            while (true)
            {
                Console.Clear();
                PrintCurrentDirectory();
                PrintFileStructure();

                string pathToInfo = Path.Combine(currentDirectory);
                FileInfo fileInfo = new FileInfo(pathToInfo);
                Console.WriteLine($"Created: {fileInfo.CreationTime}");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
                Console.WriteLine($"Path: {fileInfo.FullName}");
                DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(pathToInfo));
                Console.WriteLine($"Available Space: {driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024} Gbytes");
                Console.WriteLine($"Total Space: {driveInfo.TotalSize/1024/1024/1024} Gbytes");
                Console.WriteLine($"Drive Label: {driveInfo.VolumeLabel}");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"═════════════════════════════════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Available commands: ");
                Console.WriteLine("1. cd <directory> - Change directory");
                Console.WriteLine("2. copy <source> <destination> - Copy file or directory");
                Console.WriteLine("3. delete <path> - Delete file or directory");
                Console.WriteLine("4. info <path> - Get file or directory information");
                Console.WriteLine("5. exit - Exit the file manager");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"═════════════════════════════════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter a command: ");
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

                                string pathToInfo1 = Path.Combine(currentDirectory, commandParts[1]);

                                if (File.Exists(pathToInfo1))
                                {
                                    FileInfo fileInfo1 = new FileInfo(pathToInfo1);
                                    Console.WriteLine($"Name: {fileInfo1.Name}");
                                    Console.WriteLine($"Size: {fileInfo1.Length/1024} Kbytes");
                                    Console.WriteLine($"Attributes: {fileInfo1.Attributes}");
                                    Console.WriteLine($"Created: {fileInfo1.CreationTime}");
                                    Console.WriteLine($"Last Modified: {fileInfo1.LastWriteTime}");
                                    Console.WriteLine($"Path: {fileInfo1.FullName}");
                                }
                                else if (Directory.Exists(pathToInfo))
                                {
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathToInfo);
                                    Console.WriteLine($"Name: {directoryInfo.Name}");
                                    Console.WriteLine($"Attributes: {directoryInfo.Attributes}");
                                    Console.WriteLine($"Created: {directoryInfo.CreationTime}");
                                    Console.WriteLine($"Last Modified: {directoryInfo.LastWriteTime}");
                                    Console.WriteLine($"Path: {directoryInfo.FullName}");
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
           // Console.WriteLine($"Current Directory: {currentDirectory}");
        }

        static void PrintFileStructure()
        {
            string[] directories = Directory.GetDirectories(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory);

            int totalItems = directories.Length + files.Length;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"═════════════════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("File Structure:");
            Console.WriteLine($"Page 1 of {totalPages}");

            int currentPage = 1;
            int currentItem = 1;
            Console.WriteLine($"\n{currentDirectory}");
            foreach (string directory in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                Console.Write($" ├");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" {directoryInfo.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"   (Folder)");
                Console.Write($"   Created: {directoryInfo.CreationTime}\n");
                //Console.Write($"  Last Modified: {directoryInfo.LastWriteTime}\n");
                currentItem++;

                if (currentItem > itemsPerPage)
                {
                    currentPage++;
                    if (currentPage <= totalPages)
                    {
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        Console.Write("Press Enter to continue...");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\n═════════════════════════════════════════════════════════════════════════════════");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                        Console.Clear();
                        PrintCurrentDirectory();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"═════════════════════════════════════════════════════════════════════════════════");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("File Structure:");
                        Console.WriteLine($"Page {currentPage} of {totalPages}");
                        Console.WriteLine($"\n{currentDirectory}");
                        currentItem = 1;
                    }
                }
            }

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.Write($" ├ {Path.GetFileName(file)}");
                Console.Write($"   Size: {fileInfo.Length/1024} Kbytes");
                Console.Write($"   Created: {fileInfo.CreationTime}\n");
                //Console.Write($"   Last Modified: {fileInfo.LastWriteTime}\n");
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"═════════════════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.White;
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