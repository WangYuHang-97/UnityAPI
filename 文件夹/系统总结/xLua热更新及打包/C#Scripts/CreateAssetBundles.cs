using System.IO;
using UnityEditor;

public class CreateAssetBundles  {
    [MenuItem("Assets/Build AssetBundles")]//编辑器模式


    /// <summary>
    /// <para>打包资源方法</para>
    /// <para>BuildAssetBundleOptions.None:使用LZMA算法压缩,压缩包小但加载时间长,解压需要全部解压</para>
    /// <para>BuildAssetBundleOptions.UncompressedAssetBundle:不压缩,包大,加载快</para>
    /// <para>BuildAssetBundleOptions.ChunkBasedCompression:使用LZ4压缩,压缩包中等,可以解压指定资源</para>
    /// </summary>
    [MenuItem("Assets/Build AssetBundles")] //编辑器模式
    static void BuildAllAssetBundles()
    {
        string dir = "AssetBundles"; //指定路径
        if (Directory.Exists(dir) == false) //若路径不存在
        {
            Directory.CreateDirectory(dir); //创建路径
        }

        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64); //将资源打包至指定路径
    }
}
