using System;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioPlayOnEnable : MonoBehaviour
    {
        [SerializeField] private AudioPlay audioPlay;
        [SerializeField] private float volumeScale = 1f;
        [SerializeField] private bool loop = true;

        public void OnEnable()
        {
            AudioManager.Instance.PlayThirdSound(audioPlay.audioClip, volumeScale, true, loop);
        }

        public void OnDisable()
        {
            if (AudioManager.Instance)
                AudioManager.Instance.StopThirdSound();
        }
    }
}