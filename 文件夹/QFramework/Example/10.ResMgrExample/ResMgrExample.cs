using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
	public class ResMgrExample : MonoBehaviour
	{

#if UNITY_EDITOR
		[UnityEditor.MenuItem("QFramework/Example/10.ResMgrExample", false, 10)]
		static void MenuItem()
		{
			UnityEditor.EditorApplication.isPlaying = true;

			new GameObject("ResMgrExample")
				.AddComponent<ResMgrExample>();
		}
#endif

		ResLoader mResLoader = new ResLoader();


		private IEnumerator Start()
		{
			yield return new WaitForSeconds(2.0f);

			mResLoader.LoadAsync<AudioClip>("resources://coin", coinClip =>
			{
				Debug.Log(coinClip.name);
				
				Debug.Log(Time.time);
			});
			
			Debug.Log(Time.time);

			yield return new WaitForSeconds(2.0f);

			mResLoader.LoadSync<AudioClip>("resources://home");

			yield return new WaitForSeconds(2.0f);

			mResLoader.LoadSync<GameObject>("resources://HomePanel");
			
			mResLoader.LoadSync<AudioClip>("resources://Audio/coin");

			yield return new WaitForSeconds(5.0f);

			mResLoader.ReleaseAll();
		}
	}
}