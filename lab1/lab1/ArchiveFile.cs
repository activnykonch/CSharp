using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace lab1
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
            using (FileStream sourceStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(path))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
            CompressedFileName = path;
        }

        public void Decompress()
        {
            string path = fileInfo.FullName.TrimEnd(fileInfo.Extension.ToCharArray()) + ".txt"; //????
            using (FileStream sourceStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(path))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
            DecompressedFileName = path;
        }
    }
}
