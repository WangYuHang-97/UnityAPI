using UnityEngine;

namespace QFramework
{
	public class UIXXXPanel : MonoBehaviour
	{

#if UNITY_EDITOR
		[UnityEditor.MenuItem("QFramework/Example/9.UIXXXPanel", false, 9)]
		static void MenuItem()
		{
			UnityEditor.EditorApplication.isPlaying = true;

			new GameObject("UIXXXPanel")
				.AddComponent<UIXXXPanel>()
				.gameObject.AddComponent<UIYYYPanel>();
		}
#endif

		ResLoader mResLoader = new ResLoader();

		private void Start()
		{
			var coinClip = mResLoader.LoadSync<AudioClip>("resources://coin");

			var homeClip = mResLoader.LoadSync<AudioClip>("resources://home");

			var bgClip = mResLoader.LoadSync<AudioClip>("resources://coin");
			///

			OtherFunction();
		}


		private void OtherFunction()
		{
			var bgClip = mResLoader.LoadSync<AudioClip>("resources://coin");
		}

		private void OnDestroy()
		{
			mResLoader.ReleaseAll();
		}
	}

	public class UIYYYPanel : MonoBehaviour
	{
		ResLoader mResLoader = new ResLoader();

		private void Start()
		{
			var coinClip = mResLoader.LoadSync<AudioClip>("resources://coin");

			var homeClip = mResLoader.LoadSync<AudioClip>("resources://home");

			var bgClip = mResLoader.LoadSync<AudioClip>("resources://coin");
			///

			OtherFunction();
		}


		private void OtherFunction()
		{
			var bgClip = mResLoader.LoadSync<AudioClip>("resources://coin");
		}

		private void OnDestroy()
		{
			mResLoader.ReleaseAll();
		}
	}
}