﻿using Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptions.Encryptors
{
    class CesarEncryptor<T> : IEncryptor<T> where T : IKeyHolder
    {
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
