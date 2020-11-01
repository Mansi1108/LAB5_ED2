using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Encryptions.Encryptors
{
    public class ZigZagEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
        #region Variables
        public int height = 0;
        string encrypted = "";
        public char Fillingchar = '*';

        #endregion

        public ZigZagEncryptor(int key)
        {
            height = key;
        }

        public ZigZagEncryptor()
        {
        }

        #region Encryption

        public string EncryptFile(string savingPath, string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public void EncryptString1(string text)
        {
            List<char>[] ArrayY = new List<char>[height];
            while (text.Length > 0)
            {

                for (int i = 0; i < height - 1; i++)
                {
                    if (text.Length > 0)
                    {
                        ArrayY[i].Add(text[0]);
                        text.Remove(0, 1);
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
                        text.Remove(0, 1);
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
        }

        public string EncryptString(string text, T Key)
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
            throw new NotImplementedException();
        }

        public int GetM (string message)
        {
            int m = (message.Length / (2 + 2 * (height - 2)));
            return  m;
        }
        #endregion




    }
}
