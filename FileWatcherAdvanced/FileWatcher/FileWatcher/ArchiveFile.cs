using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Compression;

namespace Archivation
{
    class ArchiveFile
    {
        FileInfo fileInfo;

        public string CompressedFileName { get; private set; }
        public string DecompressedFileName { get; private set; }

        public ArchiveFile(string path)
        {
            fileInfo = new FileInfo(path);
        }

        public void Compress()
        {
            string path = fileInfo.FullName.TrimEnd(fileInfo.Extension.ToCharArray()) + ".zip";
            try
            {
                using (FileStream zipToCreate = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (ZipArchive zip = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                    {
                        zip.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                    }
                }
                CompressedFileName = path;
            }
            catch (Exception ex)
            {
                using (FileStream fileStream = new FileStream($"{fileInfo.Directory}" + "\\" + "log.txt", FileMode.Append))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(String.Format("{0} {1}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), ex.Message));
                    writer.Flush();
                }

            }
        }

        public void Decompress()
        {
            try
            {
                string path = default;
                using (var zip = ZipFile.OpenRead(fileInfo.FullName))
                {
                    ZipArchiveEntry file = zip.Entries.FirstOrDefault();
                    path = fileInfo.DirectoryName + "\\" + file.Name;
                    file.ExtractToFile(path);
                }
                DecompressedFileName = path;
            }
            catch (Exception ex)
            {
                using (FileStream fileStream = new FileStream($"{fileInfo.Directory}" + "\\" + "log.txt", FileMode.Append))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(String.Format("{0} {1}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), ex.Message));
                    writer.Flush();
                }

            }
            Thread.Sleep(100);
        }
    }
}
