using System;
using UnityEngine;

namespace Runtime.Audio
{
    [Serializable]
    public class AudioPlay
    {
        public AudioClip audioClip;

        public void Play(float volumeScale)
        {
            if (audioClip == null) return;
            AudioManager.Instance.PlaySound(audioClip, volumeScale);
        }

        public void Stop()
        {
            AudioManager.Instance.StopSound();
        }
    }
}