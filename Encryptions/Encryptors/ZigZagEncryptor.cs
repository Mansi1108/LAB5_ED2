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

        #endregion

        public ZigZagEncryptor()
        {
        }

        #region Encryption

        public string EncryptString(string text, T Key)
        {
            int height = Convert.ToInt32(Key);
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
            throw new NotImplementedException();
        }

        #endregion

        #region Decryption

        public string DecryptFile(string savingPath, string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public string DecryptString(string text, T Key)
        {
            int height = Convert.ToInt32(Key);
            List<char>[] ArrayY = new List<char>[height];
            bool flag = true;
            for (int i = 0; i < height; i++)
            {
                ArrayY[i] = new List<char>();
            }
            int m = GetM(text, height);
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

        public int GetM (string message, int height)
        {
            int m = (message.Length / (2 + 2 * (height - 2)));
            return  m;
        }
        #endregion




    }
}
