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

        public void SetDimensions(int height, int large)
        {
            X = height;
            Y = large;
            MatrixList = new List<byte[,]>();

        }
        #endregion

        #region File


        #endregion

        #region String

        public void SVerticalFiller(string Message)
        {
            char[,] ActualMatrix = new char[X, Y];
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
           
        }
        //No terminado
        public void SSpiralFiller(string Message)
        {

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
                    }
                    y++;
                    x++;
                }
                SMatrixList.Add(ActualMatrix);
            }
        }
        #endregion

        public string DecryptFile(string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public string DecryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }

        public string EncryptFile(string completeFilePath, T key)
        {
            throw new NotImplementedException();
        }

        public string EncryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }
    }
}
