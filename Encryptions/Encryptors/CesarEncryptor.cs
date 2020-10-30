﻿using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Encryptions.Encryptors
{
    public class CesarEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
        #region Variables
        Dictionary<byte, byte> CesarDictionary = new Dictionary<byte, byte>();
        #endregion

        #region DictionaryLoad
        private void LoadDictionary(T key, bool encryption)
        {
            var keyValue = key.GetCesarKey();
            var upperCharacters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(x => (byte)x).ToList();
            var lowerCharacters = Enumerable.Range('a', 'z' - 'a' + 1).Select(x => (byte)x).ToList();
            if (encryption)
            {
                FillDictionary(keyValue.ToUpper(), true, upperCharacters);
                FillDictionary(keyValue.ToLower(), true, lowerCharacters);
            }
            else
            {
                FillDictionary(keyValue.ToUpper(), false, upperCharacters);
                FillDictionary(keyValue.ToLower(), false, upperCharacters);
            }
        }

        private void FillDictionary(string key, bool encryption, List<byte> letterList)
        {
            var characterList = letterList;
            var keyList = new List<byte>();
            foreach (var character in key)
            {
                if (!keyList.Contains((byte)character))
                {
                    keyList.Add((byte)character);
                }
            }
            var secondaryList = new List<byte>();
            if (encryption)
            {
                for (int i = 0; i < keyList.Count; i++)
                {
                    CesarDictionary.Add(characterList[i], keyList[i]);
                }
                foreach (var character in characterList)
                {
                    if (!CesarDictionary.ContainsValue(character))
                    {
                        secondaryList.Add(character);
                    }
                }
            }
            else
            {
                for (int i = 0; i < keyList.Count; i++)
                {
                    CesarDictionary.Add(keyList[i], characterList[i]);
                }
                foreach (var character in characterList)
                {
                    if (!CesarDictionary.ContainsKey(character))
                    {
                        secondaryList.Add(character);
                    }
                }
            }
            characterList.RemoveRange(0, keyList.Count);
            if (encryption)
            {
                for (int i = 0; i < secondaryList.Count; i++)
                {
                    CesarDictionary.Add(characterList[i], secondaryList[i]);
                }
            }
            else
            {
                for (int i = 0; i < secondaryList.Count; i++)
                {
                    CesarDictionary.Add(secondaryList[i], characterList[i]);
                }
            }
        }
        #endregion

        #region Encryption
        public string EncryptFile(string savingPath, string completeFilePath, T key)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            LoadDictionary(key, true);
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath)}.csr";
            using var fileForWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileForWriting);
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                foreach (var character in buffer)
                {
                    if (CesarDictionary.ContainsKey(character))
                    {
                        writer.Write(CesarDictionary[character]);
                    }
                    else
                    {
                        writer.Write(character);
                    }
                }
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileForWriting.Close();
            return fileRoute;
        }

        public string EncryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Decryption
        public string DecryptFile(string savingPath, string completeFilePath, T key)
        {
            using var fileForReading = new FileStream(completeFilePath, FileMode.Open);
            using var reader = new BinaryReader(fileForReading);
            var buffer = new byte[2000];
            LoadDictionary(key, false);
            var fileRoute = $"{savingPath}/{Path.GetFileNameWithoutExtension(completeFilePath)}.txt";
            using var fileforWriting = new FileStream(fileRoute, FileMode.OpenOrCreate);
            using var writer = new BinaryWriter(fileforWriting);
            while (fileForReading.Position != fileForReading.Length)
            {
                buffer = reader.ReadBytes(buffer.Length);
                foreach (var character in buffer)
                {
                    if (CesarDictionary.ContainsKey(character))
                    {
                        writer.Write(CesarDictionary[character]);
                    }
                    else
                    {
                        writer.Write(character);
                    }
                }
            }
            reader.Close();
            fileForReading.Close();
            writer.Close();
            fileforWriting.Close();
            return fileRoute;
        }

        public string DecryptString(string text, T Key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
