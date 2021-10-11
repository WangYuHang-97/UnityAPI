using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEditor;
using UnityEngine;

public class CostomEditor 
{
    [MenuItem("CustomEditor/删除所有本地数据")]
    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        string path = $"{Application.persistentDataPath}/SaveData";
        if (Directory.Exists(path))
        {
            DirectoryInfo diSource = new DirectoryInfo(path);
            FileSystemInfo[] files = diSource.GetFileSystemInfos();
            for (var i = 0; i < files.Length; i++)
            {
                File.Delete(files[i].FullName);
            }
        }
        Debug.Log("删除成功");
    }

    [MenuItem("CustomEditor/导入存档")]
    public static void FileInit()
    {
        Dictionary<string, string> files = JsonMapper.ToObject<Dictionary<string, string>>(GUIUtility.systemCopyBuffer);
        string path = $"{Application.persistentDataPath}/SaveData";
        DeleteAllData();

        foreach (var file in files)
        {
            FileStream fs = new FileStream($"{path}/{file.Key}", FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(file.Value);  //这里是写入的内容             
            sw.Flush();
        }
        Debug.Log("导入成功");
    }

    [MenuItem("CustomEditor/测试")]
    public static void Test()
    {
        
    }
}
