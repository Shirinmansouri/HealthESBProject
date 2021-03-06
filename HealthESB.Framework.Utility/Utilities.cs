using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HealthESB.Framework.Utility
{
    public static class Utilities
    {
        public static byte[] KeyDES = ASCIIEncoding.ASCII.GetBytes("rvfnaped");
        public static byte[] KeySSO = ASCIIEncoding.ASCII.GetBytes("rvfnaped");
        public static byte[] KeyAgent = ASCIIEncoding.ASCII.GetBytes("rvfnaped");
        public static byte[] KeyDESAC = ASCIIEncoding.ASCII.GetBytes("rsfnaded");
        public static byte[] KeyDESNMS = ASCIIEncoding.ASCII.GetBytes("rsfnaded");
        public static string CurrentTokenAC { get; set; }
        public static string CurrentTokenNMS { get; set; }
        public static string GetPersianDateTime(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime GetDateTime(string dt)
        {
            PersianCalendar pc = new PersianCalendar();
            var data = dt.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string[] date = data[0].Split("/");
            string[] time = data[1].Split(":");
            DateTime dateTime = pc.ToDateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]), 0);
            return dateTime;
        }
        public static string SerializeObject(this object obj)
        {
            DataContractJsonSerializer jss = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                jss.WriteObject(ms, obj);
                string retVal = Encoding.UTF8.GetString(ms.ToArray());
                ms.Dispose();
                return retVal;
            }
        }
        public static T DeserializeObject<T>(this string input)
        {
            DataContractJsonSerializer jss = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                T obj = (T)jss.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
        }

        public static string Ascii2Hex(string sInt)
        {
            string str = "";
            for (int i = 0; i < sInt.Length; i++)
            {
                uint sHex = Convert.ToUInt32(Convert.ToChar(sInt.Substring(i, 1)));
                str = str + UIntToHex(sHex, 0);
            }
            return str;
        }

        public static string Decrypt(string txt, string key)
        {
            byte[] data = ToByte(txt);
            SimpleDES edes = new SimpleDES(ToByte(key));
            return hexString(edes.Decrypt(data));
        }

        public static string Encrypt(string txt, string key)
        {
            byte[] data = ToByte(txt);
            SimpleDES edes = new SimpleDES(ToByte(key));
            return hexString(edes.Encrypt(data));
        }

        private static char forDigit(int digit, int radix)
        {
            if ((radix < 2) || (radix > 0x24))
            {
                throw new ArgumentOutOfRangeException("radix");
            }
            if ((digit < 0) || (digit >= radix))
            {
                throw new ArgumentOutOfRangeException("digit");
            }
            if (digit < 10)
            {
                return (char)(digit + 0x30);
            }
            return (char)((digit - 10) + 0x61);
        }

        public static string getPin(string PB, string CardNo, string key)
        {
            try
            {
                if (CardNo.Length > 0x10)
                {
                    CardNo = CardNo.Substring(0, 0x10);
                }
                if (CardNo.Length < 0x10)
                {
                    CardNo = CardNo.PadRight(0x10, '0');
                }
                string strSource = CardNo.Substring(CardNo.Length - 13);
                strSource = strSource.Substring(0, strSource.Length - 1).PadLeft(0x10, '0');
                PB = Decrypt(PB, key);
                byte[] b = ToByte(PB);
                byte[] buffer2 = ToByte(strSource);
                for (int i = 0; i < 8; i++)
                {
                    b[i] = (byte)(b[i] ^ buffer2[i]);
                }
                PB = hexString(b);
                string s = PB.Substring(0, 2);
                if (s.Trim().Equals("0A"))
                {
                    s = "10";
                }
                if (s.Trim().Equals("0B"))
                {
                    s = "11";
                }
                if (s.Trim().Equals("0C"))
                {
                    s = "12";
                }
                PB = PB.Substring(2);
                int result = 4;
                int.TryParse(s, out result);
                return PB.Substring(0, result);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string hexString(byte[] b)
        {
            string str = "";
            for (int i = 0; i < b.Length; i++)
            {
                char ch = forDigit((b[i] >> 4) & 15, 0x10);
                char ch2 = forDigit(b[i] & 15, 0x10);
                str = str + ch.ToString().Trim().ToUpper() + ch2.ToString().Trim().ToUpper();
            }
            return str;
        }

        private static uint HexToUInt(string sHex)
        {
            return new Hex(sHex).ToUInt();
        }

        public static string setPinBlock(string CardNo, string pin, string key)
        {
            if (CardNo.Length > 0x10)
            {
                CardNo = CardNo.Substring(0, 0x10);
            }
            if (CardNo.Length < 0x10)
            {
                CardNo = CardNo.PadRight(0x10, '0');
            }
            string strSource = CardNo.Substring(CardNo.Length - 13);
            strSource = strSource.Substring(0, strSource.Length - 1).PadLeft(0x10, '0');
            string str3 = pin.Length.ToString().Trim().PadLeft(2, '0');
            if (str3.Trim().Equals("10"))
            {
                str3 = "0A";
            }
            if (str3.Trim().Equals("11"))
            {
                str3 = "0B";
            }
            if (str3.Trim().Equals("12"))
            {
                str3 = "0C";
            }
            byte[] b = ToByte((str3 + pin).PadRight(0x10, 'F'));
            byte[] buffer2 = ToByte(strSource);
            for (int i = 0; i < 8; i++)
            {
                b[i] = (byte)(b[i] ^ buffer2[i]);
            }
            return Encrypt(hexString(b), key);
        }

        public static string ToAscii(string sInt)
        {
            string str = "";
            for (int i = 0; i < sInt.Length; i += 2)
            {
                str = str + Convert.ToChar(HexToUInt(sInt.Substring(i, 2).Trim())).ToString();
            }
            return str;
        }

        public static byte[] ToByte(string strSource)
        {
            byte[] buffer = new byte[strSource.Length / 2];
            int index = 0;
            for (int i = 0; i < strSource.Length; i++)
            {
                buffer[index] = Convert.ToByte(HexToUInt(strSource.Substring(i, 2)));
                index++;
                i++;
            }
            return buffer;
        }

        public static byte[] ToBytes(string strSource)
        {
            byte[] buffer = new byte[strSource.Length];
            for (int i = 0; i < strSource.Length; i++)
            {
                uint num2 = Convert.ToUInt32(Convert.ToChar(strSource.Substring(i, 1)));
                buffer[i] = Convert.ToByte(num2);
            }
            return buffer;
        }

        public static string UIntToHex(uint sHex, int ibase)
        {
            return new Hex(sHex, ibase).ToString();
        }
        public static string GetIsActiveName(bool value)
        {
            if (value)
                return "فعال";
            else
                return "غیر فعال";
        }

        public static string GetIsDeletedName(bool value)
        {
            if (value)
                return "حذف شده";
            else
                return "حذف نشده";
        }



        public static string GetIsEditableName(bool value)
        {
            if (value)
                return "قابل ویرایش";
            else
                return "غیرقابل ویرایش";
        }
        public static string GetPadCode(string value)
        {
            return value.PadLeft(4, '0');
        }
        public enum SupplyStatusCode
        {
            Full = 0,
            Low = 1,
            Empty = 2,
            Hwerror = 3,
            NearFull = 4,
            DoorOpen = 5,
            DoorClose = 6,
            High = 7,
            Medium = 8
        }
        public static Dictionary<SupplyStatusCode, string> SupplyStatusName =
         new Dictionary<SupplyStatusCode, string>()
         {
                {SupplyStatusCode.Empty, "Empty"},
                {SupplyStatusCode.Full, "Full"},
                {SupplyStatusCode.Hwerror, "HwError"},
                {SupplyStatusCode.Low, "Low"},
                {SupplyStatusCode.NearFull, "NearFull"},
                 {SupplyStatusCode.DoorOpen, "DoorOpen"},
                  {SupplyStatusCode.DoorClose, "DoorClose"},
                   {SupplyStatusCode.High, "High"},
                    {SupplyStatusCode.Medium, "Medium"},
         };
 
    }
}
