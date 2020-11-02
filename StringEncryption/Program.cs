using System;
using API.Models;
using Encryptions.Encryptors;

namespace StringEncryption
{
    class Program
    {
        static void Main(string[] args)
        {           
            try
            {
                ZigZagEncryptor<KeyHolder> ZZE = new ZigZagEncryptor<KeyHolder>();
                Console.WriteLine("Escribe el string a cifrar");
                string text = Console.ReadLine();
                Console.WriteLine("Escribe la llave del cifrado");
                string key = Console.ReadLine();
                
                Console.WriteLine("Se ha guardado el string con éxito para cifrar");
                string CompressedText = ZZE.EncryptString(text, key);
                Console.WriteLine("El resultado de la compresión es el siguiente:");
                Console.WriteLine(CompressedText);
                Console.WriteLine("¿Desea descifrarlo? | Presione 'Y'. De lo contrario, presione cualquier otra tecla.");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine("El resultado de la descifrado es el siguiente:");
                    Console.WriteLine(ZZE.DecryptString(CompressedText, key));
                    Console.ReadLine();
                }
                Console.WriteLine("Feliz día!");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Inserte un string válido");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
