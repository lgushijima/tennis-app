using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO.Compression;
using System.IO;

namespace TennisApi.Shared.Crypto
{
    public class Crypto
    {
        private TripleDESCryptoServiceProvider TripleDES = new TripleDESCryptoServiceProvider();
        private MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
        private string key = "ASyg38D#&G2j209@7tg";

        public Crypto()
        {
        }

        public byte[] MD5Hash(string value)
        {
            byte[] byteArray = ASCIIEncoding.UTF8.GetBytes(value);
            return MD5.ComputeHash(byteArray);
        }

        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                TripleDES.Key = this.MD5Hash(key);
                TripleDES.Mode = CipherMode.ECB;
                byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(stringToEncrypt);
                return ByteArrayToHexString(TripleDES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Encrypt(string stringToEncrypt, string hashkey)
        {
            try
            {
                TripleDES.Key = this.MD5Hash(hashkey);
                TripleDES.Mode = CipherMode.ECB;
                byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(stringToEncrypt);
                return ByteArrayToHexString(TripleDES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Decrypt(string encryptedString)
        {
            try
            {
                var keycreated = this.MD5Hash(key);
                TripleDES.Key = keycreated;
                TripleDES.Mode = CipherMode.ECB;
                byte[] Buffer = HexStringToByteArray(encryptedString);
                return ASCIIEncoding.UTF8.GetString(TripleDES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        public byte[] Compress(byte[] input)
        {
            byte[] output;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress))
                {
                    gs.Write(input, 0, input.Length);
                    gs.Close();
                    output = ms.ToArray();
                }
                ms.Close();
            }
            return output;
        }

        public byte[] Decompress(byte[] input)
        {
            var output = new List<byte>();
            using (MemoryStream ms = new MemoryStream(input))
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    int readByte = gs.ReadByte();
                    while (readByte != -1)
                    {
                        output.Add((byte)readByte);
                        readByte = gs.ReadByte();
                    }
                    gs.Close();
                }
                ms.Close();
            }
            return output.ToArray();
        }

        public static string Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string CalculateMD5Hash(string input)
        {

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}