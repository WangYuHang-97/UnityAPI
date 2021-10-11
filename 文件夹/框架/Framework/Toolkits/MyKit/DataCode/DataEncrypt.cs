using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DataEncrypt
{
    internal string DesEncrypt(string pToEncrypt, string sKey)
    {
        StringBuilder ret = new StringBuilder();
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        catch
        {

        }
        return ret.ToString();
    }

    internal string DesDecrypt(string pToDecrypt, string sKey)
    {
        MemoryStream ms = new MemoryStream();
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.ECB;
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();

        }
        catch
        {

        }

        return Encoding.Default.GetString(ms.ToArray());
    }

}
