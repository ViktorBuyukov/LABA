using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Read(string dirName)
    {
        Console.Clear();
        if (Directory.Exists(dirName)) // проверка на существовании пути
        {
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(dirName);
            foreach (string s in files) // вывод имен файлов
            {
                Console.WriteLine($"{s}.");
            }
        }
        string note;
        Console.Write("Введите имя файла, который хотите открыть: ");
        note = Console.ReadLine();
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirName); 
            using FileStream fstream = File.OpenRead($"{dirName}{note}.txt"); 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length); // выполняем чтение и заносим данные
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.Clear();
                Console.WriteLine($"Файл открыт. Текст из файла:\n {textFromFile}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл с таким именем не существует.");
        }
        Console.ReadKey();
    }
    static void Write(string dirName)
    {
        Console.Clear();
        if (Directory.Exists(dirName))
        {
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(dirName);
            foreach (string s in files)
            {
                Console.WriteLine("{0}. ", s);
            }
        }
        string note;
        Console.Write("Введите имя файла, который Вы хотите открыть (или создать его): ");
        note = Console.ReadLine();
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            if (!dirInfo.Exists) // создаем файл при необходимости
            {
                dirInfo.Create();
            }
            Console.WriteLine("\nВведите строку для записи в файл:");
            string text = Console.ReadLine();
            using FileStream fstream = new FileStream($"{dirName}{note}.txt", FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes(text); // переводим строку в байты
            fstream.Seek(0, SeekOrigin.End); // устанавливаем положение потока
            fstream.Write(array, 0, array.Length); // записываем последовательность байтов 
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл с таким именем не существует.\n");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Каталог не найден");
        }
        Console.Clear();
        Console.WriteLine("\nТекст записан в файл\nПрочесть?\n1.Да\n2.Нет");
        if (Console.ReadLine() == "1")
        {
            Console.Clear();
            using FileStream fstream = File.OpenRead($"{dirName}{note}.txt");
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.Clear();
                Console.WriteLine($"Текст из файла:\n {textFromFile}");
            }
        }
        Console.ReadKey();
    }
    static void ZipUnpacking(string zipFile, string targetFolder)
    {
        ZipFile.ExtractToDirectory(zipFile, targetFolder);
    }
    static void DeleteFile(string dirName)
    {
        Directory.Delete(dirName, true);
    }
    public static void Move()
    {
        Console.Clear();
        string way = @"С:\Director\1.txt";
        string way2 = @"С:\Director\";
        if (Directory.Exists(way))
        {
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(way);
            foreach (string s in files)
            {
                Console.WriteLine("{0}. ", s);
            }
        }
        Console.Write("\nВведите имя файла, который хотите переместить (или создайте его): ");
        string name = Console.ReadLine();
        Console.Write("\nВведите новое имя файла: ");
        string name2 = Console.ReadLine();
        string path = $"{way}{name}.txt";
        string newpath = $"{way2}{name2}.txt";
        try
        {
            if (!File.Exists(path))
            {
                using FileStream fs = File.Create(path);
            }
            if (File.Exists(newpath))
                File.Delete(newpath);
            File.Move(path, newpath);
            Console.WriteLine($"\n{path} был перемещён в {newpath}.");
            if (File.Exists(path))
            {
                DeleteFile(path);
                Console.WriteLine("Файл существует.");
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
        Console.ReadKey();
    }

    static void Watch(string dirName)
    {
        Console.Clear();
        DirectoryInfo dirInfo = new DirectoryInfo(dirName);
        Console.WriteLine($"Название каталога:{dirInfo.Name}");
        Console.WriteLine($"Полное название каталога:{dirInfo.FullName}");
        Console.WriteLine($"Время создания каталога:{dirInfo.CreationTime}");
        Console.WriteLine($"Корневой каталог:{dirInfo.Root}");
        Console.ReadKey();
    }
    static void Main(string[] args)
    {
        string path = @"С:\\Director\\";
        string zipFile = @"С:\\Director\\1.zip";
        string targetFolder = @"C:\\Director\\";
        ZipUnpacking(zipFile, targetFolder); 
        string choice; // Интерфейс
        do                         
        {
            Console.Clear();
            Console.WriteLine("1.Прочитать\n"
                             + "2.Записать\n"
                             + "3.Переместить и переименовать\n"
                             + "4.Информация об изменении состоянии\n"
                             + "5.Выход\n");
            Console.Write("\nНомер задачи: ");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Read(path);
                    break;
                case "2":
                    Write(path);
                    break;
                case "3":
                    Move();
                    break;
                case "4":
                    Watch(path);
                    break;
                case "5":
                    DeleteFile(path);
                    break;
            }
        } while (choice != "5");
    }
}