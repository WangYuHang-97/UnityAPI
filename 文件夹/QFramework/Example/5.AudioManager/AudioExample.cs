using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class AudioExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/5.AudioManager", false, 5)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("AudioExample").AddComponent<AudioExample>();
        }
#endif
        protected override void OnBeforeDestroy()
        {
        }

        private void Start()
        {
            AudioManager.Instance.PlaySound("coin");
            AudioManager.Instance.PlaySound("coin");

            Delay(1.0f, () => AudioManager.Instance.PlayMusic("home", true));

            Delay(3.0f, AudioManager.Instance.PauseMusic);

            Delay(5.0f, AudioManager.Instance.ResumeMusic);

            Delay(7.0f, AudioManager.Instance.StopMusic);

            Delay(9.0f, () => { AudioManager.Instance.PlayMusic("home", true); });

            Delay(11.0f, AudioManager.Instance.MusicOff);

            Delay(13.0f, AudioManager.Instance.MusicOn);
        }
    }
}