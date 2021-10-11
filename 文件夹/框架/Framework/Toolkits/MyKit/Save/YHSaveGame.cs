using System;
using System.IO;
using LitJson;
using UnityEngine;

public enum YHSaveGamePath
{

    PersistentDataPath,

    DataPath

}

public static class YHSaveGame
{
    private static string persistentDataPath;
    private static string dataPath;

    static YHSaveGame()
    {
        persistentDataPath = Application.persistentDataPath;
        dataPath = Application.dataPath;
    }

    public static void Save<T>(string identifier, T obj,YHSaveGamePath path = YHSaveGamePath.PersistentDataPath)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            throw new System.ArgumentNullException(nameof(identifier));
        }
        string filePath = "";
        if (!IsFilePath(identifier))
        {
            switch (path)
            {
                case YHSaveGamePath.PersistentDataPath:
                    filePath = $"{persistentDataPath}/{identifier}";
                    break;
                case YHSaveGamePath.DataPath:
                    filePath = $"{dataPath}/{identifier}";
                    break;
            }
        }
        else
        {
            filePath = identifier;
        }
        Stream stream = File.Create(filePath);
        if (obj == null)
        {
            obj = default(T);
        }
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
        YHSaveGameJsonSerializer serializer = new YHSaveGameJsonSerializer();
        serializer.Serialize(obj, stream);
        stream.Dispose();
    }

    public static T Load<T>(string identifier, Func<T> defaultValue, YHSaveGamePath path = YHSaveGamePath.PersistentDataPath)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            throw new System.ArgumentNullException(nameof(identifier));
        }
        string filePath = "";
        if (!IsFilePath(identifier))
        {
            switch (path)
            {
                case YHSaveGamePath.PersistentDataPath:
                    filePath = $"{persistentDataPath}/{identifier}";
                    break;
                case YHSaveGamePath.DataPath:
                    filePath = $"{dataPath}/{identifier}";
                    break;
            }
        }
        else
        {
            filePath = identifier;
        }
        if (File.Exists(filePath))
        {
            Stream stream = File.OpenRead(filePath);
            YHSaveGameJsonSerializer serializer = new YHSaveGameJsonSerializer();
            var result = serializer.Deserialize<T>(stream);
            stream.Dispose();
            stream.Close();
            return result;
        }
        else
        {
            var result = defaultValue.Invoke();
           Save(identifier, result, path);
            return result;
        }
        
    }

    static bool IsFilePath(string str)
    {
        bool result = false;
        if (Path.IsPathRooted(str))
        {
            try
            {
                Path.GetFullPath(str);
                result = true;
            }
            catch (System.Exception)
            {
                result = false;
            }
        }
        return result;
    }
}
