using System.IO;
using UnityEngine;

namespace QFramework
{
	public class AssetBundleExample : MonoBehaviour
	{

#if UNITY_EDITOR
		[UnityEditor.MenuItem("QFramework/Example/11.AssetBundleExample/Build AssetBundle", false, 11)]
		static void MenuItem1()
		{
			if (!Directory.Exists(Application.streamingAssetsPath))
			{
				Directory.CreateDirectory(Application.streamingAssetsPath);
			}

			UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, UnityEditor.BuildAssetBundleOptions.None,
				UnityEditor.BuildTarget.StandaloneWindows);
		}
		
		[UnityEditor.MenuItem("QFramework/Example/11.AssetBundleExample/Run", false, 12)]
		static void MenuItem2()
		{
			UnityEditor.EditorApplication.isPlaying = true;

			new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
		}
#endif

		private ResLoader mResLoader = new ResLoader();
		
		private AssetBundle mBundle;
		
		// Use this for initialization
		void Start()
		{
			mBundle = mResLoader.LoadSync<AssetBundle>( "gameobject");

			var gameObj = mBundle.LoadAsset<GameObject>("GameObject");


			Instantiate(gameObj);

		}

		private void OnDestroy()
		{
			mBundle = null;
			
			mResLoader.ReleaseAll();
			mResLoader = null;
		}
	}
}