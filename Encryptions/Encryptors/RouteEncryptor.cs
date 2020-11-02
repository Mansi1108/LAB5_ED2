using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Encryptions.Encryptors
{
    public class RouteEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
        #region Variables
        public int X = 0;
        public int Y = 0;
        public List<byte[,]> FMatrixList = new List<byte[,]>();
        public List<char[,]> SMatrixList = new List<char[,]>();
        public char Fillingchar = '*';
        #endregion

        #region Building
        public RouteEncryptor(int height, int large)
        {
            X = height;
            Y = large;
        }

        //Alternativa
        public RouteEncryptor()
        {

        }

        public void SetVariables(int height, int large)
        {
            X = large;
            Y = height;
            List<char[,]> SMatrixList = new List<char[,]>();
            List<byte[,]> FMatrixList = new List<byte[,]>();
        }
        #endregion

        #region File


        #endregion

        #region String

        public string SVerticalFiller(string Message)
        {
            char[,] ActualMatrix = new char[X, Y];
            string encryptedmessagge = "";
            while (Message.Length > 0)
            {
              ActualMatrix = new char[X, Y];
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                    }
                }
                SMatrixList.Add(ActualMatrix);
            }
            foreach (var item in SMatrixList)
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        encryptedmessagge += item[j, i];
                    }
                }
            }
            SetVariables(0,0);
            return encryptedmessagge;
        }
        //No terminado
        public string SSpiralFiller(string Message)
        {
            string encryptedmessagge = "";
            char[,] ActualMatrix = new char[X, Y];
            while (Message.Length > 0)
            {
                int MaxCapacity = X * Y;
                int x = 0;
                int y = 0;
                ActualMatrix = new char[X, Y];
                while (MaxCapacity>0)
                {
                    int i = x;
                    int j = y;
                    for (i = x; i < X - x; i++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    for (j = y + 1; j < Y - y; j++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    for (i = X - 2 - x; i >= x; i--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    for (j = Y - 2 - y; j > y; j--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    y++;
                    x++;
                }
                SMatrixList.Add(ActualMatrix);
            }
            foreach (var item in SMatrixList)
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        encryptedmessagge += item[j, i];
                    }
                }
            }
            SetVariables(0, 0);
            return encryptedmessagge;
        }
        #endregion
        public string SDecryptvertical(string Message, int height, int large)
        {
            SetVariables(height, large);
            char[,] ActualMatrix = new char[X, Y];
            string desencryptedmessagge = "";
            while (Message.Length > 0)
            {
                ActualMatrix = new char[X, Y];
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        ActualMatrix[j, i] = Message[0];
                        Message.Remove(0, 1);
                    }
                }
                SMatrixList.Add(ActualMatrix);
            }
            foreach (var item in SMatrixList)
            {
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        desencryptedmessagge += item[i, j];
                    }
                }
            }
            return desencryptedmessagge;
        }
        
        public string SDecryptSpiral(string Message, int height, int large)
        {
            SetVariables(height, large);
            char[,] ActualMatrix = new char[X, Y];
            string desencryptedmessagge = "";
            while (Message.Length > 0)
            {
                ActualMatrix = new char[X, Y];
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        ActualMatrix[j, i] = Message[0];
                        Message.Remove(0, 1);
                    }
                }
                SMatrixList.Add(ActualMatrix);
            }
            int Maxcapacity = X * Y;
            foreach (var item in SMatrixList)
            {
                int x = X;
                int y = Y;
                while (Maxcapacity > 0)
                {
                    int j = x;
                    int i = y;
                    
                    for (i = x; i < X - x; i++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        Maxcapacity--;
                    }
                    for (j = y + 1; j < Y - y; j++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        Maxcapacity--;
                    }
                    for (i = X - 2 - x; i >= x; i--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        Maxcapacity--;
                    }
                    for (j = Y - 2 - y; j > y; j--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        Maxcapacity--;
                    }
                    y++;
                    x++;
                }
            
            }
            return desencryptedmessagge;
        }
        public string DecryptFile(string savingPath, string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public string DecryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }

        public string EncryptFile(string savingPath, string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public string EncryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }
    }
}
