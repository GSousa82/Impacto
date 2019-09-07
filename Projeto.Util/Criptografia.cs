using System;
using System.Text;
using System.Security.Cryptography;

namespace Projeto.Util
{
    public class Criptografia
    {
        //método para retornar um valor encriptado com padrão HASH MD5(Message Digest)
        public static string MD5Encrypti(string value)
        {
            //convertendo para Byte
            var valueInBytes = Encoding.UTF8.GetBytes(value);

            //aplicar a criptografia
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(valueInBytes);

            //retornar o valor da criptografia em string
            var result = string.Empty;

            foreach (var pos in hash)
            {
                result += pos.ToString("X2");//X2 -> hexadecimal
            }

            return result;
        }
    }
}
