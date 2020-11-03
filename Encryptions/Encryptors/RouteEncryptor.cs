using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System.Diagnostics;

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
            X = large;
            Y = height;
        }

        //Alternativa
        public RouteEncryptor()
        {

        }

        public void SetVariables(int height, int large)
        {
            X = large;
            Y = height;
            SMatrixList = new List<char[,]>();
            FMatrixList = new List<byte[,]>();
        }
        #endregion

        #region File
        public string FVerticalEncrypt(string savingPath, string completeFilePath, T key)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath)}";
            using var fileForWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileForWriting);
            byte[,] ActualMatrix = new byte[X, Y];
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                int k = 0;
                while (k < buffer.Length)
                {
                    ActualMatrix = new byte[X, Y];

                    for (int i = 0; i < X; i++)
                    {
                        for (int j = 0; j < Y; j++)
                        {
                            if (k < buffer.Length)
                            {
                                ActualMatrix[i, j] = buffer[k];
                                k++;
                            }
                            else
                            {
                                ActualMatrix[i, j] = Convert.ToByte(Fillingchar);
                            }
                        }
                    }
                    FMatrixList.Add(ActualMatrix);
                }
            }
            foreach (var item in FMatrixList)
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        writer.Write(item[j, i]);
                    }
                }
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileForWriting.Close();
            return fileRoute;
        }
        public string FSpiralEncrypt(string savingPath, string completeFilePath)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath) + ".rt"}";
            using var fileForWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileForWriting);
            byte[,] ActualMatrix = new byte[X, Y];
            while (fileForReading.Position != fileForReading.Length)
            {
                int k = 0;
                buffer = reader.ReadBytes(buffer.Length);
                while (k < buffer.Length)
                {
                    int MaxCapacity = X * Y;
                    int x = 0;
                    int y = 0;
                    ActualMatrix = new byte[X, Y];
                    while (MaxCapacity > 0)
                    {
                        int i = x;
                        int j = y;
                        for (i = x; i < X - x; i++)
                        {
                            if (k < buffer.Length)
                            {
                                ActualMatrix[i, j] = buffer[k];
                                k++;
                            }
                            else
                            {
                                ActualMatrix[i, j] = Convert.ToByte(Fillingchar);
                            }
                            MaxCapacity--;
                        }
                        i--;
                        for (j = y + 1; j < Y - y; j++)
                        {

                            if (k < buffer.Length)
                            {
                                ActualMatrix[i, j] = buffer[k];
                                k++;
                            }
                            else
                            {
                                ActualMatrix[i, j] = Convert.ToByte(Fillingchar);
                            }
                            MaxCapacity--;
                        }
                        j--;
                        for (i = X - 2 - x; i > x; i--)
                        {

                            if (k < buffer.Length)
                            {
                                ActualMatrix[i, j] = buffer[k];
                                k++;
                            }
                            else
                            {
                                ActualMatrix[i, j] = Convert.ToByte(Fillingchar);
                            }
                            MaxCapacity--;
                        }
                        for (j = Y - 1 - y; j > y; j--)
                        {

                            if (k < buffer.Length)
                            {
                                ActualMatrix[i, j] = buffer[k];
                                k++;
                            }
                            else
                            {
                                ActualMatrix[i, j] = Convert.ToByte(Fillingchar);
                            }
                            MaxCapacity--;
                        }
                        y++;
                        x++;
                    }
                    FMatrixList.Add(ActualMatrix);
                }
            }
            foreach (var item in FMatrixList)
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        writer.Write(item[j, i]);
                    }
                }
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileForWriting.Close();
            return fileRoute;
        }
        public string FDecryptvertical(string savingPath, string completeFilePath, T key)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath)}";
            using var fileforWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileforWriting);
            byte[,] ActualMatrix = new byte[X, Y];
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                int k = 0;
                while (k < buffer.Length)
                {
                    ActualMatrix = new byte[X, Y];
                    for (int i = 0; i < Y; i++)
                    {
                        for (int j = 0; j < X; j++)
                        {
                            ActualMatrix[j, i] = buffer[k];
                            k++;
                        }
                    }
                    FMatrixList.Add(ActualMatrix);
                }
            }
            foreach (var item in SMatrixList)
            {
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        if (item[i, j] != Convert.ToByte(Fillingchar))
                        {
                            writer.Write(item[j, i]);
                        }
                    }
                }
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileforWriting.Close();
            return fileRoute;
        }
        public string FDecryptSpiral(string savingPath, string completeFilePath)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath) + ".txt"}";
            using var fileforWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileforWriting);
            byte[,] ActualMatrix = new byte[X, Y];
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                int k = 0;
                while (k < buffer.Length)
                {
                    ActualMatrix = new byte[X, Y];
                    for (int i = 0; i < Y; i++)
                    {
                        for (int j = 0; j < X; j++)
                        {
                            ActualMatrix[j, i] = buffer[k];
                            k++;
                        }
                    }
                    FMatrixList.Add(ActualMatrix);
                }
            }
            foreach (var item in FMatrixList)
            {
                int x = 0;
                int y = 0;
                int Maxcapacity = X * Y;
                while (Maxcapacity > 0)
                {
                    int j = x;
                    int i = y;

                    for (i = x; i < X - x; i++)
                    {
                        if (item[i, j] != Convert.ToByte(Fillingchar))
                        {
                            writer.Write(item[i, j]);
                        }
                        Maxcapacity--;
                    }
                    i--;
                    for (j = y + 1; j < Y - y; j++)
                    {
                        if (item[i, j] != Convert.ToByte(Fillingchar))
                        {
                            writer.Write(item[i, j]);
                        }
                        Maxcapacity--;
                    }
                    j--;
                    for (i = X - 2 - x; i > x; i--)
                    {
                        if (item[i, j] != Convert.ToByte(Fillingchar))
                        {
                            writer.Write(item[i, j]);
                        }
                        Maxcapacity--;
                    }
                    for (j = Y - 1 - y; j > y; j--)
                    {
                        if (item[i, j] != Convert.ToByte(Fillingchar))
                        {
                            writer.Write(item[i, j]);
                        }
                        Maxcapacity--;
                    }
                    y++;
                    x++;
                }

            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileforWriting.Close();
            return fileRoute;
        }
        #endregion

        #region String

        public string SVerticalEncrypter (string Message)
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
                            Message = Message.Remove(0, 1);
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
        public string SSpiralEncrypter(string Message)
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
                            Message = Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    i--;
                    for (j = y + 1; j < Y - y; j++)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message = Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    j--;
                    for (i = X - 2 - x; i > x; i--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message = Message.Remove(0, 1);
                        }
                        else
                        {
                            ActualMatrix[i, j] = Fillingchar;
                        }
                        MaxCapacity--;
                    }
                    for (j = Y - 1 - y; j > y; j--)
                    {
                        if (Message.Length > 0)
                        {
                            ActualMatrix[i, j] = Message[0];
                            Message = Message.Remove(0, 1);
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
                        Message = Message.Remove(0, 1);
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
                        if (item[i,j] != Fillingchar)
                        {
                            desencryptedmessagge += item[i, j];
                        }  
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
                        Message = Message.Remove(0, 1);
                    }
                }
                SMatrixList.Add(ActualMatrix);
            }
            foreach (var item in SMatrixList)
            {
                int x = 0;
                int y = 0;
                int Maxcapacity = X * Y;
                while (Maxcapacity > 0)
                {
                    int j = x;
                    int i = y;

                    for (i = x; i < X - x; i++)
                    {
                        if (item[i,j] != Fillingchar)
                        {
                            desencryptedmessagge += item[i, j];
                        }
                        Maxcapacity--;
                    }
                    i--;
                    for (j = y + 1; j < Y - y; j++)
                    {
                        if (item[i, j] != Fillingchar)
                        {
                            desencryptedmessagge += item[i, j];
                        }
                        Maxcapacity--;
                    }
                    j--;
                    for (i = X - 2 - x; i > x; i--)
                    {
                        if (item[i, j] != Fillingchar)
                        {
                            desencryptedmessagge += item[i, j];
                        }
                        Maxcapacity--;
                    }
                    for (j = Y - 1 - y; j > y; j--)
                    {
                        if (item[i, j] != Fillingchar)
                        {
                            desencryptedmessagge += item[i, j];
                        }
                        Maxcapacity--;
                    }
                    y++;
                    x++;
                }

            }
            return desencryptedmessagge;
        }
        #endregion
        public string DecryptFile(string savingPath, string completeFilePath, T key)
        {
            var K = key.GetRouteKey();
            SetVariables(K[1], K[0]);
            return FDecryptSpiral(savingPath, completeFilePath);
            throw new NotImplementedException();

        }
        public string DecryptString(string text, T Key)
        {
            var K = Key.GetRouteKey();
            SetVariables(K[1], K[0]);
            throw new NotImplementedException();
        }

        public string EncryptFile(string savingPath, string completeFilePath, T key)
        {
            var K = key.GetRouteKey();
            SetVariables(K[1], K[0]);
            return FSpiralEncrypt(savingPath, completeFilePath);
            throw new NotImplementedException();

        }

        public string EncryptString(string text, T Key)
        {
            var K = Key.GetRouteKey();
            SetVariables(K[1], K[0]);
            throw new NotImplementedException();
        }
    }
}
