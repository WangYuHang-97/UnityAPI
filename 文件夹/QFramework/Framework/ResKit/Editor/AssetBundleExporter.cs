using System.IO;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
	public class AssetBundleExpoter 
	{
		[MenuItem("QFramework/Framework/ResKit/Build AssetBundles", false)]
		static void BuildAssetBundles()
		{
			var outputPath = Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName();

			if (!Directory.Exists(outputPath))
			{
				Directory.CreateDirectory(outputPath);
			}

			BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
				EditorUserBuildSettings.activeBuildTarget);

			AssetDatabase.Refresh();
		}
	}
}