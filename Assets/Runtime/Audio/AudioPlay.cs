using System;
using UnityEngine;

namespace Runtime.Audio
{
    [Serializable]
    public class AudioPlay
    {
        public AudioClip audioClip;

        public void Play()
        {
            if (audioClip == null) return;
            AudioManager.Instance.PlaySound(audioClip);
        }

        public void Stop()
        {
            AudioManager.Instance.StopSound();
        }
    }
}