using Core;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioSource bgMusic;
        [SerializeField] private AudioSource sound;

        public void PlayBgMusic()
        {
            bgMusic.Play();
        }

        public void ChangeBgMusic(AudioClip music)
        {
            var currentVolume = bgMusic.volume;
            
            DOTween.Sequence()
                .Append(DOTween.To(x => bgMusic.volume = x, currentVolume, 0, 0.5f))
                .AppendCallback(() => bgMusic.clip = music)
                .Append(DOTween.To(x => bgMusic.volume = x, 0, currentVolume, 0.5f))
                .AppendCallback(() => bgMusic.Play())
                .Play();
        }

        public void PlaySound(AudioClip audioClip)
        {
            if (sound.isPlaying) return;
            sound.PlayOneShot(audioClip);
        }

        public void StopSound()
        {
            sound.Stop();
        }
    }
}