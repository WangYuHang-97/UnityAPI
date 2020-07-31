using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// xLua热更新基类,全局唯一,用于调用xlua方法并回收
/// </summary>
[Hotfix()]//需要通过xLua修改的类都必须添加
public class HotFixScript : MonoBehaviour
{
    #region 字段
    //引入模块
    private LuaEnv luaEnv;//引入LuaEnv模块,全局唯一

    //变量
    public static Dictionary<string,object> prefabDict = new Dictionary<string, object>();
    #endregion

    #region 方法
    /// <summary>
    /// 用于调用lua文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static byte[] Myloader(ref string filePath)
    {
        string absPath = @"H:\Lua\FishingJoy\" + filePath + ".lua.txt";//文件路径
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }

    // ************************************示例,用内存加载AssetBundle资源并使用,其他方法请参考AssetBundlesAPI类
    /// <summary>
    /// <para>同步从内存加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para>
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="filePath"></param>
    [LuaCallCSharp()]//可能需要重写的文件必须加上(空方法不用)
    public static void LoadAssetBundleFromMemory(string resName, string filePath)
    {
        string path = "AssetBundles/" + filePath; //资源路径
        AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
        AssetBundle manifestAB =
            AssetBundle.LoadFromMemory(File.ReadAllBytes(System.Environment.CurrentDirectory + "AssetBundles")); //加载AssetBundles
        AssetBundleManifest
            manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); //加载AssetBundles.manifest
        string[] strs = manifest.GetAllDependencies(filePath); //获取该物体依赖物体数组
        foreach (string str in strs) //分别加载
        {
            AssetBundle.LoadFromMemory(File.ReadAllBytes(System.Environment.CurrentDirectory + "AssetBundles" + str));
        }
        GameObject Prefab = ab.LoadAsset<GameObject>(resName); //提取AssetBundle内容        
        prefabDict.Add(resName, Prefab);//添加至字典方便调用
    }
    // ************************************示例,用内存加载AssetBundle资源并使用

    /// <summary>
    /// <para>传递名称返回所需要物体</para>
    /// <para>objName:打包物体名称,即Unity中Inspector名称</para>
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public static object GetObject(string objName)
    {
        return prefabDict[objName];
    }
    #endregion

    #region Unity回调

    void Start()
    {
        luaEnv = new LuaEnv();//初始化LuaEnv模块
        luaEnv.AddLoader(Myloader);//添加lua存放文件夹
        luaEnv.DoString("require 'Fish'");//配合luaEnv.AddLoader(Myloader)添加名为 Fish 的lua文件,并执行该lua文件内容(调用Fish.lua)
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        luaEnv.DoString("require 'FishDispose'");//回收lua文件(调用FishDispose.lua)
    }

    void OnDestroy()
    {
        luaEnv.Dispose();//确保lua文件
    }
    #endregion
}
