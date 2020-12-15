using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Northwind.FileManagerService.EncryptionTools
{
    class EncryptFile
    {
        FileInfo fileInfo;
        string key;
        string iv;

        public EncryptFile(string path, string key = "YoungHeffner2019", string iv = "Cadillac")
        {
            fileInfo = new FileInfo(path);
            this.key = key;
            this.iv = iv;
        }

        public void Encrypt()
        {
            try
            {
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.ISO10126;
                    rijAlg.Key = new UnicodeEncoding().GetBytes(this.key);
                    rijAlg.IV = new UnicodeEncoding().GetBytes(this.iv);
                    byte[] encrypted;
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, rijAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(File.ReadAllText(fileInfo.FullName));
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                    using (FileStream myStream = new FileStream(fileInfo.FullName, FileMode.Truncate))
                    {
                        foreach (byte bt in encrypted)
                        {
                            myStream.WriteByte(bt);
                        }
                    }
                }
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

        public void Decrypt()
        {
            try
            {
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.ISO10126;
                    rijAlg.Key = new UnicodeEncoding().GetBytes(this.key);
                    rijAlg.IV = new UnicodeEncoding().GetBytes(this.iv);
                    string decrypted;
                    using (MemoryStream msDecrypt = new MemoryStream(File.ReadAllBytes(fileInfo.FullName)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, rijAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                decrypted = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                    using (FileStream myStream = new FileStream(fileInfo.FullName, FileMode.Truncate))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(myStream))
                        {
                            streamWriter.Write(decrypted);
                        }
                    }
                }
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
    }
}
