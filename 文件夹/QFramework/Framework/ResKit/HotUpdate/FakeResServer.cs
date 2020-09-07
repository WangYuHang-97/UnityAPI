using System;
using System.Collections;
using UnityEngine;

namespace QFramework
{
	[Serializable]
	public class ResVersion
	{
		public int Version;
	}
	
	public class FakeResServer : MonoSingleton<FakeResServer>
	{
		public void GetRemoteResVersion(Action<int> onResVersionGet)
		{
			StartCoroutine(RequestRemoteResVersion(resVersion => onResVersionGet(resVersion.Version)));
		}

		public void DownloadRes(Action<ResVersion> onResDownloaded)
		{
			StartCoroutine(RequestRemoteResVersion(onResDownloaded));
		}

		private IEnumerator RequestRemoteResVersion(Action<ResVersion> onResVerionGetted)
		{
			var remoteResVersionPath =
				Application.dataPath + "/QFramework/Framework/ResKit/HotUpdate/RemoteResVersion.json";

			var www = new WWW(remoteResVersionPath);
			yield return www;

			var jsonString = www.text;

			var resVersion = JsonUtility.FromJson<ResVersion>(jsonString);
			onResVerionGetted(resVersion);
		}
	}
}