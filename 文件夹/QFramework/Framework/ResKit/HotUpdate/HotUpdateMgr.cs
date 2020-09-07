using System;
using System.IO;
using UnityEngine;

namespace QFramework
{
	public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
	{
		public int GetLocalResVersion()
		{
			var jsonString = File.ReadAllText(Application.streamingAssetsPath + "/AssetBundles/Windows/ResVersion.json");
			var resVersion = JsonUtility.FromJson<ResVersion>(jsonString);
			return resVersion.Version;
		}

		public void HasNewVersionRes(Action<bool> onResultGetted)
		{
			FakeResServer.Instance.GetRemoteResVersion(remoteResVersion =>
			{
				var result = remoteResVersion > GetLocalResVersion();
				onResultGetted(result);
			});
		}
		
		public void UpdateRes(Action onUpdateDone)
		{
			Debug.Log("开始更新");
			Debug.Log("1.下载资源");
			
			FakeResServer.Instance.DownloadRes(resVersion =>
			{
				ReplaceLocalRes(resVersion);
				Debug.Log("结束更新");
				onUpdateDone();
			});
		}

		void ReplaceLocalRes(ResVersion resVersion)
		{
			Debug.Log("2.替换本地资源");
			var localResVersionPath = Application.streamingAssetsPath + "/AssetBundles/Windows/ResVersion.json";
			var jsonContent = JsonUtility.ToJson(resVersion);
			File.WriteAllText(localResVersionPath,jsonContent);	
		}
	}
}