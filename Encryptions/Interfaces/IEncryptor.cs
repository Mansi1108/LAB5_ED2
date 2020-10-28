using System;
using System.Collections.Generic;
using System.Text;

namespace Encryptions.Interfaces
{
    interface IEncryptor<T>
    {
        string EncryptFile(string completeFilePath, T key);
        string DecryptFile(string completeFilePath, T key);
        string EncryptString(string text, T Key);
        string DecryptString(string text, T Key);
    }
}
