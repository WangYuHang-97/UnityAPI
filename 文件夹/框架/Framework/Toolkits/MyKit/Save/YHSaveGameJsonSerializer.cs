using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FullSerializer;
using LitJson;
using UnityEngine;

public class YHSaveGameJsonSerializer
{
    public void Serialize<T>(T obj, Stream stream)
    {
#if !UNITY_WSA || !UNITY_WINRT
        try
        {
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            string jsonData = JsonMapper.ToJson(obj);//先json
            writer.Write(DataEncryptManager.StringEncoder(jsonData));
            writer.Dispose();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
#else
			StreamWriter writer = new StreamWriter ( stream, encoding );
			writer.Write ( JsonUtility.ToJson ( obj ) );
			writer.Dispose ();
#endif
    }

    public T Deserialize<T>(Stream stream)
    {
        T result = default(T);
#if !UNITY_WSA || !UNITY_WINRT
        try
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            result = JsonMapper.ToObject<T>(DataEncryptManager.StringDecoder(reader.ReadToEnd()));
            if (result == null)
            {
                result = default(T);
            }
            reader.Dispose();
        }
        catch (Exception ex)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            Debug.Log(reader.ReadToEnd());
            Debug.LogException(ex);
        }
#else
			StreamReader reader = new StreamReader ( stream, encoding );
			result = JsonUtility.FromJson<T> ( reader.ReadToEnd () );
			reader.Dispose ();
#endif
        return result;
    }
}
