using System;
using UnityEngine;

public static class DataEncryptManager
{
    private static string _stringKey = "luzhuang";
    private static readonly DataEncrypt DataEncrypt = new DataEncrypt();

    public static string StringEncoder(string str)
    {
        return DataEncrypt.DesEncrypt(str, _stringKey);
    }

    public static string StringEncoder(int num)
    {
        return DataEncrypt.DesEncrypt(num.ToString(), _stringKey);
    }

    public static string StringEncoder(float num)
    {
        return DataEncrypt.DesEncrypt(num.ToString(), _stringKey);
    }

    public static string StringDecoder(string str)
    {
        return DataEncrypt.DesDecrypt(str, _stringKey);
    }

    public static int StringToIntDecoder(string str)
    {
        string value = StringDecoder(str);
        if (value == String.Empty && value == "")
        {
            return 0;
        }
        else
        {
            try
            {
                if (value.Contains("."))
                {
                    return (int)float.Parse(value);
                }
                else
                {
                    return Int32.Parse(value);
                }
            }
            catch (Exception e)
            {
                Debug.Log("目标文本" + value);
                Debug.LogError("获得数值失败" + e);
                return -99999999;
            }
        }
    }

    public static float StringToFloatDecoder(string str)
    {
        string value = StringDecoder(str);
        if (value == String.Empty && value == "")
        {
            return 0;
        }
        try
        {
            return float.Parse(value);
        }
        catch (Exception e)
        {
            Debug.Log("目标文本" + value);
            Debug.LogError("获得数值失败" + e);
            return -99999999;
        }
    }

    public static string StringEncoder(object num)
    {
        throw new NotImplementedException();
    }
}
