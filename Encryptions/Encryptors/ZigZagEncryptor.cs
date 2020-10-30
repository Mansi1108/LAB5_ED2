using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptions.Encryptors
{
    public class ZigZagEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
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
