using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tryme
{
    public class Crypt
    {
        string _key;
        string _folderPath;
        bool _deleteFiles;

        public Crypt(string folderPath, bool deleteFiles, bool showKey)
        {
            _folderPath = folderPath;
            _key = RandomString(64);

            if (showKey)
                Console.WriteLine($"Generated key: {_key}");
            else
            {
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "key.txt"), _key);
            }

            _deleteFiles = deleteFiles;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void EncryptFolder(string vault)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_key);

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                string[] files = Directory.GetFiles(_folderPath, "*", SearchOption.AllDirectories);

                //foreach (string file in files)
                //{
                //    Console.WriteLine($"Encrypting file: {file}");
                //}


                writer.Write(files.Length);

                foreach (string file in files)
                {
                    string relativePath = Path.GetRelativePath(_folderPath, file);

                    //Console.WriteLine($"Processing file: {relativePath}");

                    byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath);

                    byte[] fileBytes = File.ReadAllBytes(file);

                    writer.Write(pathBytes.Length);
                    writer.Write(pathBytes);

                    writer.Write(fileBytes.Length);
                    writer.Write(fileBytes);
                }

                byte[] containerData = ms.ToArray();
                byte[] encryptedData = SimpleXor(containerData, keyBytes);

                File.WriteAllBytes(vault, encryptedData);
            }


            DeleteFiles(_deleteFiles);
        }

        public void DecryptFolder(string vaultPath, string outputFolder)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Console.ReadLine());

            byte[] encryptedData = File.ReadAllBytes(vaultPath);
            byte[] decryptedData = SimpleXor(encryptedData, keyBytes);

            using (MemoryStream ms = new MemoryStream(decryptedData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                int fileCount = reader.ReadInt32();

                for (int i = 0; i < fileCount; i++)
                {
                    int pathLength = reader.ReadInt32();
                    byte[] pathBytes = reader.ReadBytes(pathLength);
                    string relativePath = Encoding.UTF8.GetString(pathBytes);

                    int fileLength = reader.ReadInt32();
                    byte[] fileBytes = reader.ReadBytes(fileLength);

                    string fullPath = Path.Combine(outputFolder, relativePath);
                    string directory = Path.GetDirectoryName(fullPath);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    File.WriteAllBytes(fullPath, fileBytes);
                }
            }
        }

        private byte[] SimpleXor(byte[] data, byte[] keyBytes)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                byte keyByte = keyBytes[i % keyBytes.Length];
                result[i] = (byte)(data[i] ^ keyByte);
            }

            return result;
        }

        public void EncryptFile()
        {
            byte[] data = File.ReadAllBytes(_folderPath);
            byte[] keyBytes = Encoding.UTF8.GetBytes(_key);

            byte[] encryptedData = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                byte keyByte = keyBytes[i % keyBytes.Length];

                byte xored = (byte)(data[i] ^ keyByte);

                encryptedData[i] = xored;
            }

            //File.WriteAllBytes(_path, encryptedData);
        }


        private void ShowBytes(byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                Console.Write($"{b:X2} ");
            }

        }


        private void DeleteFiles(bool delete)
        {
            if (delete)
            {
                    Directory.Delete(_folderPath, true);
            }
        }
    }
}
