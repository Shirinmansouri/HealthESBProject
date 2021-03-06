using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HealthESB.Framework.Utility
{
    public class SimpleDES
    {
        private DESCryptoServiceProvider des;
        private readonly byte[] IV = new byte[8];
        private byte[] mKey;

        public SimpleDES(byte[] aKey)

        {
            this.mKey = aKey;
            this.des = new DESCryptoServiceProvider();
            this.des.Padding = PaddingMode.None;
            this.des.Mode = CipherMode.CBC;
        }

        public byte[] Decrypt(byte[] data)
        {
            if (data.Length != 8)
            {
                throw new Exception("Data size must be 8 bytes");
            }
            return this.des.CreateDecryptor(this.mKey, this.IV).TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {

            return this.des.CreateEncryptor(this.mKey, this.IV).TransformFinalBlock(data, 0, data.Length);
        }
        public string EncryptAnyData(string data)
        {
            this.des.Padding = PaddingMode.PKCS7;
            var buffer = Encoding.UTF8.GetBytes(data);
            var myencrypt = des.CreateEncryptor(this.mKey, this.IV);
            buffer = myencrypt.TransformFinalBlock(buffer, 0, buffer.Length);

            return Utilities.hexString(buffer);
        }
        public string DecryptAnyData(string data)
        {
            this.des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.CBC;
            var buffer = Utilities.ToByte(data);
            var mydecrypt = des.CreateDecryptor(this.mKey, this.IV);
            buffer = mydecrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer).Replace("\u0004", "");
        }
        public string Encrypt(string PlainText)
        {


            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(encStream);
            sw.WriteLine(PlainText);
            sw.Close();
            encStream.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return Convert.ToBase64String(buffer);
        }
        public string EncryptAnyData(byte[] buffer)
        {
            var myencrypt = des.CreateEncryptor(this.mKey, this.IV);
            buffer = myencrypt.TransformFinalBlock(buffer, 0, buffer.Length);

            return Utilities.hexString(buffer);
        }
        public string DecryptAnyData(byte[] buffer)
        {
            var mydecrypt = des.CreateDecryptor(this.mKey, this.IV);
            buffer = mydecrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return Utilities.hexString(buffer);
        }
    }
}
