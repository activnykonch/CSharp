using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    class Human
    {
        public Human()
        {
            DateOfBirth = DateTime.Now;
        }
        public Human(string name)
        {
            Name = name;
            DateOfBirth = DateTime.Now;
        }
        public Human(string name, DateTime date)
        {
            Name = name;
            DateOfBirth = date;
        }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        //------------------------------------------------
        public static void Write(List<Human> humans, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                foreach (Human human in humans)
                {
                    sw.WriteLine(human.Name);
                    sw.WriteLine(human.DateOfBirth.ToString());
                }
            }
        }
        public static void Read(List<Human> humans, string path)
        {
            string name;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((name = sr.ReadLine()) != null)
                {
                    humans.Add(new Human(name, Convert.ToDateTime(sr.ReadLine())));
                }
            }
        }
        public static void BinWrite(List<Human> humans, string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (Human human in humans)
                {
                    writer.Write(human.Name);
                    writer.Write(human.DateOfBirth.ToString());
                }
            }
        }
        public static void BinRead(List<Human> humans, string path)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    humans.Add(new Human(reader.ReadString(), Convert.ToDateTime(reader.ReadString())));
                }
            }
        }
    }
}
