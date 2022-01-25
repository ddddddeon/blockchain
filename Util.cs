using System.Text;

namespace Blockchain
{
    public static class Util
    {
        public static string BytesToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static byte[] StringToBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str, 0, str.Length);
        }

        public static string BytesToString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        }
    }
}