using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    class Program
    {
        public static void DirInfo(StringBuilder path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path.ToString());

            if (!dirInfo.Exists)
            {
                Console.WriteLine("Вы совершили переход в несуществующую директорию" + "\n//-------------------------------------------------------");
                FileExplorer.Return(path);
            }
            Console.WriteLine("Подкаталоги:");
            string[] dirs = Directory.GetDirectories(path.ToString());
            foreach (string s in dirs)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(path.ToString());
            foreach (string s in files)
            {
                Console.WriteLine(s);
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("\n\n1. Move\n2. Return\n3. Create directory\n4. Delete directory\n5. Save file\n6. Upload File\n7. Delete File\n8. Copy File\n9. Compressing\n10. Restoring\n11. Rename\n");
        }

        static void Main(string[] args)
        {
            List<Human> humen = new List<Human>()
            {
                new Human("2D"),
                new Human("Murdoch"),
                new Human("Russell"),
                new Human("Noodle")
            };
            List<Human> testHumen = new List<Human>();
            StringBuilder path = new StringBuilder();
            StringBuilder oldPath = new StringBuilder();
            string newName;
            FileExplorer.Start(path);
            string name;
            while (true)
            {
                Console.WriteLine("Our human list");
                foreach (Human human in testHumen)
                {
                    Console.WriteLine(human.Name + '\n' + human.DateOfBirth.ToString());
                }
                ShowMenu();
                DirInfo(path);
                Console.WriteLine("//--------------------------------------------------\n" + path);
                int com = 0;
                if (!int.TryParse(Console.ReadLine(), out com))
                {
                    Console.Clear();
                    Console.WriteLine("!!!Некорректная команда!!!");
                    continue;
                }
                switch (com)
                {
                    case 1:
                        name = Console.ReadLine();
                        FileExplorer.Move(path, name);
                        break;
                    case 2:
                        FileExplorer.Return(path);
                        break;
                    case 3:
                        name = Console.ReadLine();
                        FileExplorer.DirCreation(path, name);
                        break;
                    case 4:
                        name = Console.ReadLine();
                        FileExplorer.DirDestruction(path, name);
                        break;
                    case 5:
                        name = Console.ReadLine();
                        if (!FileExplorer.SaveFile(humen, path, name))
                        {
                            Console.Clear();
                            Console.WriteLine("Некорректный формат файла");
                            Console.ReadKey();
                        }
                        break;
                    case 6:
                        name = Console.ReadLine();
                        if (!FileExplorer.UploadFile(testHumen, path, name))
                        {
                            Console.Clear();
                            Console.WriteLine("Некорректный формат файла");
                            Console.ReadKey();
                        }
                        break;
                    case 7:
                        name = Console.ReadLine();
                        if (!FileExplorer.DeleteFile(path, name))
                        {
                            Console.Clear();
                            Console.WriteLine("Данный файл не существует");
                            Console.ReadKey();
                        }
                        break;
                    case 8:
                        name = Console.ReadLine();
                        if (!name.Contains('.'))
                            break;
                        oldPath = new StringBuilder(path.ToString());
                        FileExplorer.Move(oldPath, name);
                        if (!File.Exists(oldPath.ToString()))
                        {
                            Console.Clear();
                            Console.WriteLine("Данный файл не существует");
                            Console.ReadKey();
                            break;
                        }
                        FileExplorer.Return(oldPath);
                        int chose = 0;
                        while (chose != 3)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Move\n2. Return\n3. Paste");
                            DirInfo(path);
                            if (!int.TryParse(Console.ReadLine(), out chose))
                            {
                                Console.Clear();
                                Console.WriteLine("Некорректная команда");
                                break;
                            }
                            switch (chose)
                            {
                                case 1:
                                    string dir = Console.ReadLine();
                                    FileExplorer.Move(path, dir);
                                    break;
                                case 2:
                                    FileExplorer.Return(path);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (!FileExplorer.CopyFile(oldPath, path, name))
                        {
                            Console.Clear();
                            Console.WriteLine("Данный файл существует в заданной дирректории");
                            Console.ReadKey();
                        }
                        break;
                    case 9:
                        name = Console.ReadLine();
                        if (!name.Contains(".class"))
                            break;
                        oldPath = new StringBuilder(path.ToString());
                        FileExplorer.Move(oldPath, name);
                        newName = Console.ReadLine();
                        if (!newName.Contains(".zcl"))
                            break;
                        FileExplorer.Move(path, newName);
                        FileExplorer.Archiving(oldPath, path);
                        FileExplorer.Return(path);
                        break;
                    case 10:
                        name = Console.ReadLine();
                        if (!name.Contains(".zcl"))
                            break;
                        oldPath = new StringBuilder(path.ToString());
                        FileExplorer.Move(oldPath, name);
                        newName = Console.ReadLine();
                        if (!newName.Contains(".class"))
                            break;
                        FileExplorer.Move(path, newName);
                        FileExplorer.Dearchiving(oldPath, path);
                        FileExplorer.Return(path);
                        break;
                    case 11:
                        name = Console.ReadLine();
                        if (!name.Contains(".class"))
                            break;
                        oldPath = new StringBuilder(path.ToString());
                        FileExplorer.Move(oldPath, name);
                        newName = Console.ReadLine();
                        if (!newName.Contains(".class"))
                            break;
                        FileExplorer.Move(path, newName);
                        File.Move(oldPath.ToString(), path.ToString());
                        FileExplorer.Return(path);
                        break;
                }
                Console.Clear();
            }
        }
    }
}
