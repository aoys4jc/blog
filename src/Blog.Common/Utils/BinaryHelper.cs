using System;

namespace Blog.Common
{
    public class BinaryHelper
    {
        public static string BinaryToHexString(ref byte[] pData, int offset, int size)
        {
            if (null == pData || 0 >= size ||
               offset < 0 || pData.Length < (offset + size))
                return null;

            string hex = "";
            for (int i = offset; i < offset + size; i++)
                hex += pData[i].ToString("X2");

            return hex;
        }

        public static string BinaryToBitString(ref byte[] pData, int offset, int size)
        {
            if (null == pData || 0 >= size ||
                offset < 0 || pData.Length < (offset + size))
                return null;

            string bits = "";
            for (int i = offset; i < offset + size; i++)
                bits += Convert.ToString(pData[i], 2).PadLeft(8, '0');

            return bits;
        }

        public static byte[] HexStringToBinary(string hexStr, int offset, int size)
        {
            if (string.IsNullOrEmpty(hexStr) || 0 >= size ||
               size % 2 != 0 || offset < 0 || hexStr.Length < (offset + size))
                return null;

            byte[] pResult = new byte[size / 2];
            for (int i = 0; i < size / 2; i++)
                pResult[i] = Convert.ToByte(hexStr.Substring(offset + (i * 2), 2), 16);

            return pResult;
        }

        public static byte[] Int32ToBinary(int value)
        {
            byte[] bits = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bits);

            return bits;
        }

        public static void ReverseBytes(ref byte[] pData)
        {
            if (null == pData || 0 == pData.Length)
                return;

            byte t = 0;
            for (int i = 0; i < pData.Length / 2; i++)
            {
                t = pData[i];
                pData[i] = pData[pData.Length - (i + 1)];
                pData[pData.Length - (i + 1)] = t;
            }
        }
    };
}
