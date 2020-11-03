using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Encryptions.Encryptors
{
    public class ZigZagEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
        #region Variables
        public int height = 0;
        string encrypted = "";
        string decrypted = "";
        public char Fillingchar = '*';
        public ZigZagEncryptor()
        {
        }

        #endregion


        #region Encryption

        public string EncryptString(string text, T Key)
        {
            var height = Key.GetZigZagKey();
            List<char>[] ArrayY = new List<char>[height];
            for (int i = 0; i < height; i++)
            {
                ArrayY[i] = new List<char>();
            }
            while (text.Length > 0)
            {
                for (int i = 0; i < height - 1; i++)
                {
                    if (text.Length > 0)
                    {
                        ArrayY[i].Add(text[0]);
                        text = text.Remove(0, 1);
                    }
                    else
                    {
                        ArrayY[i].Add(Fillingchar);

                    }
                }
                for (int j = height - 1; j > 0; j--)
                {
                    if (text.Length > 0)
                    {
                        ArrayY[j].Add(text[0]);
                        text = text.Remove(0, 1);
                    }
                    else
                    {
                        ArrayY[j].Add(Fillingchar);
                    }
                }

            }
            for (int i = 0; i < height; i++)
            {
                foreach (var item in ArrayY[i])
                {
                    encrypted += item;
                }
            };
            return encrypted;
        }

        public string EncryptFile(string savingPath, string completeFilePath, T key)
        {
            var height = key.GetZigZagKey();
            var fillingByte = Convert.ToByte(Fillingchar);
            List<byte>[] arrayY = new List<byte>[height];
            int k = 0;
            List<byte> list;

            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath)+".zz"}";
            using var fileForWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileForWriting);
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                list = new List<byte>(buffer);
                for (int i = 0; i < height; i++)
                {
                    arrayY[i] = new List<byte>();
                }
                while (list.Count > 0)
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        if (list.Count > 0)
                        {
                            arrayY[i].Add(buffer[k]);
                            k++;
                            list.RemoveAt(0);

                        }
                        else
                        {
                            arrayY[i].Add(fillingByte);

                        }
                    }
                    for (int j = height - 1; j > 0; j--)
                    {
                        if (list.Count > 0)
                        {
                            arrayY[j].Add(buffer[k]);
                            k++;
                            list.RemoveAt(0);
                        }
                        else
                        {
                            arrayY[j].Add(fillingByte);
                        }
                    }

                }
                for (int i = 0; i < height; i++)
                {
                    foreach (var item in arrayY[i])
                    {
                        writer.Write(item);
                    }
                };
                
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileForWriting.Close();
            return fileRoute;
        }

        #endregion

        #region Decryption

        public string DecryptFile(string savingPath, string completeFilePath, T key)
        {
            var height = key.GetZigZagKey();
            List<byte>[] arrayY = new List<byte>[height];
            int n = 0;
            List<byte> list;

            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath) +".txt"}";
            using var fileForWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileForWriting);

            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                list = new List<byte>(buffer);
                bool flag = true;

                for (int i = 0; i < height; i++)
                {
                    arrayY[i] = new List<byte>();
                }
                int m = GetM(buffer.Length, height);
                for (int j = 0; j < height; j++)
                {
                    if (j == 0 || j == height - 1)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            arrayY[j].Add(buffer[n]);
                            n++;
                            list.RemoveAt(0);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < (2 * m); k++)
                        {
                            arrayY[j].Add(buffer[n]);
                            n++;
                            list.RemoveAt(0);
                        }
                    }
                }
                while (flag)
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        if (arrayY[i][0].Equals(Convert.ToByte('*')))
                        {
                            flag = false;
                        }
                        else
                        {
                            writer.Write(arrayY[i][0]);
                            arrayY[i].RemoveAt(0);
                        }
                    }
                    for (int j = height - 1; j > 0; j--)
                    {
                        if (arrayY[j][0].Equals(Convert.ToByte('*')))
                        {
                            flag = false;
                        }
                        else
                        {
                            writer.Write(arrayY[j][0]);
                            arrayY[j].RemoveAt(0);
                        }
                    }
                }

            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileForWriting.Close();
            return fileRoute;
        }

        public string DecryptString(string text, T Key)
        {
            var height = Key.GetZigZagKey();
            var mLenght = text.Length;
            List<char>[] ArrayY = new List<char>[height];
            bool flag = true;
            for (int i = 0; i < height; i++)
            {
                ArrayY[i] = new List<char>();
            }
            int m = GetM(mLenght, height);
            for (int j = 0; j < height; j++)
            {
                if (j == 0 || j == height - 1)
                {
                    for (int k = 0; k < m; k++)
                    {
                        ArrayY[j].Add(text[0]);
                        text = text.Remove(0, 1);
                    }
                }
                else
                {
                    for (int k = 0; k < (2 * m); k++)
                    {
                        ArrayY[j].Add(text[0]);
                        text = text.Remove(0, 1);
                    }
                }
            }
            while (flag)
            {
                for (int i = 0; i < height - 1; i++)
                {
                    if (ArrayY[i][0].Equals('*'))
                    {
                        flag = false;
                    }
                    else
                    {
                        decrypted += ArrayY[i][0];
                        ArrayY[i].RemoveAt(0);
                    }
                }
                for (int j = height - 1; j > 0; j--)
                {
                    if (ArrayY[j][0].Equals('*'))
                    {
                        flag = false;
                    }
                    else
                    {
                        decrypted += ArrayY[j][0];
                        ArrayY[j].RemoveAt(0);
                    }
                }
            }
            return decrypted;
        }

        public int GetM (int mLenght, int height)
        {
            int m = (mLenght / (2 + 2 * (height - 2)));
            return  m;
        }

        #endregion




    }
}
