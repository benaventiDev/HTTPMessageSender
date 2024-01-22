using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HTTPMessageSender.file
{
    public class Crypto
    {
        private static byte[] AesEncrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        private static byte[] AesDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        public static void EncryptCsvFile(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
        {
            var data = File.ReadAllBytes(inputFilePath);
            var encryptedData = AesEncrypt(data, key, iv);
            File.WriteAllBytes(outputFilePath, encryptedData);
        }

        public static void DecryptCsvFile(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
        {
            var encryptedData = File.ReadAllBytes(inputFilePath);
            var decryptedData = AesDecrypt(encryptedData, key, iv);
            File.WriteAllBytes(outputFilePath, decryptedData);
        }

        public static string DecryptCsvFileToString(string inputFilePath, byte[] key, byte[] iv)
        {
            var encryptedData = File.ReadAllBytes(inputFilePath);
            var decryptedData = AesDecrypt(encryptedData, key, iv);
            return Encoding.UTF8.GetString(decryptedData);
        }


    }
}
