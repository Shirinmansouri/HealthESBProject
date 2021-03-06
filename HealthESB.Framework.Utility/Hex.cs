using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Framework.Utility
{
    public class Hex
    {
        private byte[] bytes;

        public Hex()
        {
            this.bytes = null;
        }

        public Hex(Hex hexData)
        {
            this.bytes = hexData.ToBytes();
        }

        public Hex(string data)
        {
            this.bytes = this.ParseByteString(data);
        }

        public Hex(byte[] byteArray)
        {
            this.bytes = this.CopyBytes(byteArray);
        }

        public Hex(int data, int numBytes)
        {
            string format = string.Format("X{0}", numBytes * 2);
            if (data < 0)
            {
                data += (int)Math.Pow(2.0, (double)(numBytes * 8));
            }
            string str2 = data.ToString(format);
            this.bytes = this.ParseByteString(str2);
        }

        public Hex(uint data, int numBytes)
        {
            string format = string.Format("X{0}", numBytes * 2);
            string str2 = data.ToString(format);
            this.bytes = this.ParseByteString(str2);
        }

        private byte[] CopyBytes(byte[] byteArray)
        {
            byte[] buffer = null;
            int length = byteArray.Length;
            if (length > 0)
            {
                buffer = new byte[length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = byteArray[i];
                }
            }
            return buffer;
        }

        private byte[] ParseByteString(string data)
        {
            byte[] buffer = null;
            int num = 0;
            if ((data != null) && (data.Length > 0))
            {
                if ((data.Length % 2) != 0)
                {
                    data = "0" + data;
                }
                num = data.Length / 2;
                buffer = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    string str = data.Substring(i * 2, 2);
                    buffer[i] = Convert.ToByte(str, 0x10);
                }
            }
            return buffer;
        }

        public byte[] ToBytes()
        {
            byte[] buffer = null;
            if ((this.bytes != null) && (this.bytes.Length > 0))
            {
                buffer = new byte[this.bytes.Length];
                for (int i = 0; i < this.bytes.Length; i++)
                {
                    buffer[i] = this.bytes[i];
                }
            }
            return this.bytes;
        }

        public int ToInt()
        {
            return new Bits(this.ToBytes()).ToInt();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if ((this.bytes != null) && (this.bytes.Length > 0))
            {
                for (int i = 0; i < this.bytes.Length; i++)
                {
                    builder.Append(this.bytes[i].ToString("X2"));
                }
            }
            return builder.ToString();
        }

        public uint ToUInt()
        {
            return new Bits(this.ToBytes()).ToUInt();
        }

        public int Count
        {
            get
            {
                if (this.bytes == null)
                {
                    return 0;
                }
                return this.bytes.Length;
            }
        }
    }
}
