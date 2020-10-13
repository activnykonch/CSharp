using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    static class FileExplorer
    {
        private static int position = 0;

        public static void Start(StringBuilder path)
        {
            path.Append("C:\\");
        }
        //-----------------------------------------------------------------------------------------------------------------
        //Navigation
        public static bool Move(StringBuilder path, string dir)
        {
            if (dir.Length == 0)
                return false;
            if (dir.Contains(':'))
            {
                path.Clear();
                path.Append(dir + '\\');
                position = 0;
                return true;
            }
            if (path.Length != 3)
                path.Append('\\');
            position = path.Length - 1;
            path.Append(dir);
            return true;
        }
        public static bool Return(StringBuilder path)
        {
            if (path.Length == 3)
                return false;
            path.Remove(position, path.Length - position);
            position--;
            while (path[position] != '\\' && path.Length != 2)
                position--;
            if (path.Length == 2)
                path.Append('\\');
            return true;
        }
        //End of navigation
        //-----------------------------------------------------------------------------------------------------------------
        //Dir work
        public static void DirCreation(StringBuilder path, string dirName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path.ToString());
            directoryInfo.CreateSubdirectory(dirName);
        }
        public static void DirDestruction(StringBuilder path, string dirName)
        {
            Move(path, dirName);
            Directory.Delete(path.ToString(), true);
            Return(path);
        }
        //End work creation
        //-----------------------------------------------------------------------------------------------------------------
        //File work
        public static bool SaveFile(List<Human> humen, StringBuilder path, string fileName)
        {
            if (!fileName.Contains(".class"))
                return false;
            Move(path, fileName);
            Human.Write(humen, path.ToString());
            Return(path);
            return true;
        }
        public static bool UploadFile(List<Human> humen, StringBuilder path, string fileName)
        {
            if (!fileName.Contains(".class"))
                return false;
            Move(path, fileName);
            if (!File.Exists(path.ToString()))
            {
                Return(path);
                return false;
            }
            Human.Read(humen, path.ToString());
            Return(path);
            return true;
        }
        public static bool DeleteFile(StringBuilder path, string fileName)
        {
            Move(path, fileName);
            if (!File.Exists(path.ToString()))
            {
                Return(path);
                return false;
            }
            File.Delete(path.ToString());
            Return(path);
            return true;
        }
        public static bool CopyFile(StringBuilder oldPath, StringBuilder newPath, string fileName)
        {
            Move(oldPath, fileName);
            Move(newPath, fileName);
            if (File.Exists(newPath.ToString()))
            {
                return false;
            }
            File.Copy(oldPath.ToString(), newPath.ToString());
            Return(newPath);
            return true;
        }
        //End file work
        public static void Archiving(StringBuilder path, StringBuilder newPath)
        {
            using (FileStream sourceStream = new FileStream(path.ToString(), FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(newPath.ToString()))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }
        public static void Dearchiving(StringBuilder path, StringBuilder newPath)
        {
            using (FileStream sourceStream = new FileStream(path.ToString(), FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(newPath.ToString()))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }
    }
}
