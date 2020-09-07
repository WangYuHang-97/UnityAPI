using UnityEngine;

namespace QFramework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioListener mAudioListener;

        void CheckAudioListener()
        {
            if (!mAudioListener)
            {
                mAudioListener = FindObjectOfType<AudioListener>();
            }

            if (!mAudioListener)
            {
                mAudioListener = gameObject.AddComponent<AudioListener>();
            }
        }
   
        public void PlaySound(string soundName)
        {
            CheckAudioListener();

            var coinClip = Resources.Load<AudioClip>(soundName);
            var audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip = coinClip;
            audioSource.Play();
        }

        private AudioSource mMusicSource;

        public void PlayMusic(string musicName, bool loop)
        {
            CheckAudioListener();

            if (!mMusicSource)
            {
                mMusicSource = gameObject.AddComponent<AudioSource>();
            }

            var coinClip = Resources.Load<AudioClip>(musicName);

            mMusicSource.clip = coinClip;
            mMusicSource.loop = loop;
            mMusicSource.Play();
        }

        public void StopMusic()
        {
            mMusicSource.Stop();
        }

        public void PauseMusic()
        {
            mMusicSource.Pause();
        }

        public void ResumeMusic()
        {
            mMusicSource.UnPause();
        }

        public void MusicOff()
        {
            mMusicSource.Pause();
            mMusicSource.mute = true;
        }

        public void SoundOff()
        {
            var soundSources = GetComponents<AudioSource>();

            foreach(var soundSource in soundSources)
            {
                if (soundSource != mMusicSource)
                {
                    soundSource.Pause();
                    soundSource.mute = true;
                }
            }
        }

        public void MusicOn()
        {
            mMusicSource.UnPause();
            mMusicSource.mute = false;
        }

        public void SoundOn()
        {
            var soundSources = GetComponents<AudioSource>();

            foreach (var soundSource in soundSources)
            {
                if (soundSource != mMusicSource)
                {
                    soundSource.UnPause();
                    soundSource.mute = false;
                }
            }
        }
    }
}