using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

/// <summary>
/// <para>AssetBundle模块</para>
/// <para>包括资源的打包与加载使用</para>
/// </summary>
class AssetBundlesAPI
{
    /// <summary>
    /// 用于存储所有打包对象,此方法不调用,来源于HotFixScript类,防止复制时报错
    /// </summary>
    public static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    /// <summary>
    /// <para>同步从内存加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para>
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="filePath"></param>
    [LuaCallCSharp()]
    public static void LoadAssetBundleFromMemory(string resName, string filePath)
    {
        string path = "AssetBundles/"+filePath; //资源路径
        AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
        AssetBundle manifestAB =
            AssetBundle.LoadFromMemory(File.ReadAllBytes(System.Environment.CurrentDirectory+"AssetBundles")); //加载AssetBundles
        AssetBundleManifest
            manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); //加载AssetBundles.manifest
        string[] strs = manifest.GetAllDependencies(filePath); //获取该物体依赖物体数组
        foreach (string str in strs) //分别加载
        {
            AssetBundle.LoadFromMemory(File.ReadAllBytes(System.Environment.CurrentDirectory + "AssetBundles" + str));
        }
        GameObject Prefab = ab.LoadAsset<GameObject>(resName); //提取AssetBundle内容        
        prefabDict.Add(resName,Prefab);//添加至字典方便调用
    }

    /// <summary>
    /// <para>异步从内存加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para>
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    [LuaCallCSharp()]
    IEnumerator LoadAssetBundleFromMemoryAsync(string resName, string filePath)
    {
        string path = "AssetBundles/" + filePath; //资源路径
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path)); //创建request
        yield return request; //异步返回时间
        AssetBundle ab = request.assetBundle; //赋值给AssetBundle
        AssetBundleCreateRequest requestManifestAB =
            AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(System.Environment.CurrentDirectory + "AssetBundles")); //创建Manifest-request
        yield return requestManifestAB; //异步返回时间
        AssetBundle manifestAB = requestManifestAB.assetBundle; //赋值给AssetBundle
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

    /// <summary>
    /// <para>同步从文件加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para> 
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="filePath"></param>
    [LuaCallCSharp()]
    public static void LoadAssetBundleFromFile(string resName, string filePath)
    {
        string path = "AssetBundles/" + filePath; //资源路径
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        AssetBundle manifestAB = AssetBundle.LoadFromFile(System.Environment.CurrentDirectory + "AssetBundles"); //加载AssetBundles
        AssetBundleManifest
            manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); //加载AssetBundles.manifest
        string[] strs = manifest.GetAllDependencies(filePath); //获取该物体依赖物体数组
        foreach (string str in strs) //分别加载
        {
            AssetBundle.LoadFromFile(System.Environment.CurrentDirectory + "AssetBundles" + str);
        }

        GameObject Prefab = ab.LoadAsset<GameObject>(resName); //提取AssetBundle内容
        prefabDict.Add(resName, Prefab);//添加至字典方便调用
    }

    /// <summary>
    /// <para>异步从文件加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para> 
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    IEnumerator LoadAssetBundleFromFileAsync(string resName, string filePath)
    {
        string path = "AssetBundles/" + filePath; //资源路径
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path); //创建request
        yield return request; //异步返回时间
        AssetBundle ab = request.assetBundle; //赋值给AssetBundle
        AssetBundleCreateRequest
            requestManifestAB = AssetBundle.LoadFromFileAsync(System.Environment.CurrentDirectory + "AssetBundles"); //创建Manifest-request
        yield return requestManifestAB; //异步返回时间
        AssetBundle manifestAB = requestManifestAB.assetBundle; //赋值给AssetBundle
        AssetBundleManifest
            manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); //加载AssetBundles.manifest
        string[] strs = manifest.GetAllDependencies(filePath); //获取该物体依赖物体数组
        foreach (string str in strs) //分别加载
        {
            AssetBundle.LoadFromFile(System.Environment.CurrentDirectory + "AssetBundles" + str);
        }

        GameObject Prefab = ab.LoadAsset<GameObject>(resName); //提取AssetBundle内容
        prefabDict.Add(resName, Prefab);//添加至字典方便调用
    }

    /// <summary>
    /// <para>通过服务器加载AssetBundle资源并使用</para>
    /// <para>resName:打包物体名称,即Unity中Inspector名称</para>
    /// <para>resName:AssetBundle填写路径</para> 
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator UnityWebRequest(string resName, string url)
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url); //创建request
        yield return request.SendWebRequest(); //异步返回时间
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle; //赋值给AssetBundle
        GameObject Prefab = ab.LoadAsset<GameObject>(resName); //提取AssetBundle内容
        prefabDict.Add(resName, Prefab);//添加至字典方便调用
    }
}
