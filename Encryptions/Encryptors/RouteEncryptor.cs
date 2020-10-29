using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Encryptions.Encryptors
{
    class RouteEncryptor
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

        public void SFillVertical(string Message)
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
        public void SFillSpiral(string Message)
        {

            char[,] ActualMatrix = new char[X, Y];
            while (Message.Length > 0)
            {
                ActualMatrix = new char[X, Y];
                int i = 0;
                int j = 0;
                for (i = 0; i < X; i++)
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
                for (j = 0; j < Y; j++)
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
                for (i = X-2; i >= 0; i--)
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
                for (j = Y-2; j > 0; j++)
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
                SMatrixList.Add(ActualMatrix);
            }
        }
        #endregion
    }
}
