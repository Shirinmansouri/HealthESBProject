using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Framework.Utility
{
    public class Bits
    {
        private bool[] _bitArray;

        private Bits()
        {
        }

        public Bits(Hex hexData)
        {
            this.createArray(hexData.ToBytes());
        }

        public Bits(string data)
        {
            if (!this.ValidBitString(data))
            {
                throw new ApplicationException("Invalid binary number: " + data);
            }
            this._bitArray = new bool[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '1')
                {
                    this._bitArray[i] = true;
                }
                else
                {
                    this._bitArray[i] = false;
                }
            }
        }

        public Bits(bool[] boolBits)
        {
            if (boolBits.Length <= 0)
            {
                throw new ApplicationException("Can't initialize Bits because the input arry of boolBits can't be zero length");
            }
            this._bitArray = new bool[boolBits.Length];
            for (int i = 0; i < this._bitArray.Length; i++)
            {
                this._bitArray[i] = boolBits[i];
            }
        }

        public Bits(byte[] byteArray)
        {
            if (byteArray.Length <= 0)
            {
                throw new ApplicationException("Can't convert to bits a zero length array of bytes");
            }
            this.createArray(byteArray);
        }

        public Bits(int data, int numBits)
        {
            if (numBits == 0)
            {
                throw new ApplicationException("Can't initialize bits when number of bits is 0");
            }
            if (data < 0)
            {
                data += Convert.ToInt32(Math.Pow(2.0, (double)numBits));
            }
            this._bitArray = new bool[numBits];
            int index = 0;
            int num2 = Convert.ToInt32(Math.Pow(2.0, (double)(numBits - 1)));
            for (int i = numBits - 1; i >= 0; i--)
            {
                if ((data & num2) > 0)
                {
                    this._bitArray[index] = true;
                }
                else
                {
                    this._bitArray[index] = false;
                }
                index++;
                num2 = num2 >> 1;
            }
        }

        public Bits(uint data, int numBits)
        {
            if (numBits == 0)
            {
                throw new ApplicationException("Can't initialize bits when number of bits is 0");
            }
            this._bitArray = new bool[numBits];
            int index = 0;
            uint num2 = Convert.ToUInt32(Math.Pow(2.0, (double)(numBits - 1)));
            for (int i = numBits - 1; i >= 0; i--)
            {
                if ((data & num2) > 0)
                {
                    this._bitArray[index] = true;
                }
                else
                {
                    this._bitArray[index] = false;
                }
                index++;
                num2 = num2 >> 1;
            }
        }

        private void createArray(byte[] byteArray)
        {
            this._bitArray = new bool[byteArray.Length * 8];
            int index = 0;
            for (int i = 0; i < byteArray.Length; i++)
            {
                byte num = byteArray[i];
                byte num2 = 0x80;
                for (int j = 7; j >= 0; j--)
                {
                    if ((num & num2) > 0)
                    {
                        this._bitArray[index] = true;
                    }
                    else
                    {
                        this._bitArray[index] = false;
                    }
                    index++;
                    num2 = (byte)(num2 >> 1);
                }
            }
        }

        public Bits GetBits(int bytePosition, int bitPosition, int numBits)
        {
            int num = ((bytePosition + 1) * 8) - 1;
            int num2 = (num - (bitPosition + numBits)) + 1;
            int num3 = num - bitPosition;
            if ((num2 < 0) || (num3 > (this._bitArray.Length - 1)))
            {
                throw new ApplicationException("Unable to extract bits because the combination of byte position, bit position, and number of bits is not valid");
            }
            bool[] boolBits = new bool[numBits];
            int index = 0;
            for (int i = num2; i <= num3; i++)
            {
                boolBits[index] = this._bitArray[i];
                index++;
            }
            return new Bits(boolBits);
        }

        public bool[] ToBoolArray()
        {
            bool[] flagArray = null;
            if (this._bitArray.Length > 0)
            {
                flagArray = new bool[this._bitArray.Length];
                for (int i = 0; i < this._bitArray.Length; i++)
                {
                    flagArray[i] = this._bitArray[i];
                }
            }
            return flagArray;
        }

        public byte[] ToBytes()
        {
            byte[] array = null;
            if (this._bitArray != null)
            {
                byte num = 0;
                int num2 = ((this._bitArray.Length - 1) / 8) + 1;
                array = new byte[num2];
                int num3 = 0;
                int index = 0;
                for (int i = this._bitArray.Length - 1; i >= 0; i--)
                {
                    num = (byte)(num >> 1);
                    if (this._bitArray[i])
                    {
                        num = (byte)(num | 0x80);
                    }
                    num3++;
                    if ((num3 % 8) == 0)
                    {
                        array[index] = num;
                        num = 0;
                        index++;
                    }
                }
                if ((num3 % 8) != 0)
                {
                    num = (byte)(num >> (8 - (num3 % 8)));
                }
                if (index < num2)
                {
                    array[index] = num;
                }
                Array.Reverse(array);
            }
            return array;
        }

        public Hex ToHex()
        {
            return new Hex(this.ToBytes());
        }

        public int ToInt()
        {
            int num = Convert.ToInt32(this.ToUInt());
            int length = this._bitArray.Length;
            int num3 = Convert.ToInt32((double)(Math.Pow(2.0, (double)(length - 1)) - 1.0));
            if (num > num3)
            {
                num -= Convert.ToInt32(Math.Pow(2.0, (double)length));
            }
            return num;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this._bitArray.Length > 0)
            {
                for (int i = 0; i < this._bitArray.Length; i++)
                {
                    if (this._bitArray[i])
                    {
                        builder.Append('1');
                    }
                    else
                    {
                        builder.Append('0');
                    }
                }
            }
            return builder.ToString();
        }

        public uint ToUInt()
        {
            uint num = 0;
            if (this._bitArray != null)
            {
                for (int i = 0; i < this._bitArray.Length; i++)
                {
                    num = num << 1;
                    if (this._bitArray[i])
                    {
                        num |= 1;
                    }
                }
            }
            return num;
        }

        public bool ValidBitString(string data)
        {
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if ((data[i] != '0') && (data[i] != '1'))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int Count
        {
            get
            {
                if (this._bitArray == null)
                {
                    return 0;
                }
                return this._bitArray.Length;
            }
        }
    }
}
