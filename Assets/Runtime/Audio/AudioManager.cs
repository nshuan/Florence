using Core;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioSource bgMusic;
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioSource thirdSound;
        
        public void PlayBgMusic()
        {
            bgMusic.Play();
        }

        public void ChangeBgMusic(AudioClip music, float volume = 0.3f)
        {
            DOTween.Sequence()
                .Append(DOTween.To(x => bgMusic.volume = x, bgMusic.volume, 0, 0.5f))
                .AppendCallback(() => bgMusic.clip = music)
                .Append(DOTween.To(x => bgMusic.volume = x, 0, volume, 0.5f))
                .AppendCallback(() => bgMusic.Play())
                .Play();
        }

        public void VolumeOffBgMusic()
        {
            DOTween.Sequence()
                .Append(DOTween.To(x => bgMusic.volume = x, bgMusic.volume, 0, 0.5f))
                .Play();
        }

        public void SetBgMusicAndOn(AudioClip music, float volume = 0.3f)
        {
            DOTween.Sequence()
                .AppendCallback(() => bgMusic.clip = music)
                .Append(DOTween.To(x => bgMusic.volume = x, 0, volume, 0.5f))
                .AppendCallback(() => bgMusic.Play())
                .Play();
        }

        public void PlaySound(AudioClip audioClip, float volume = 0.5f, bool restart = false)
        {
            if (!restart && sound.isPlaying) return;
            sound.volume = volume;
            sound.PlayOneShot(audioClip);
        }

        public void StopSound()
        {
            sound.Stop();
        }

        public void PlayThirdSound(AudioClip audioClip, float volume = 0.3f, bool fadeIn = true, bool loop = false)
        {
            if (!fadeIn)
            {
                thirdSound.clip = audioClip;
                thirdSound.loop = loop;
                thirdSound.Play();
                return;
            }
            
            var seq = DOTween.Sequence();
            
            if (thirdSound.isPlaying) seq.Append(DOTween.To(x => thirdSound.volume = x, thirdSound.volume, 0, 0.5f));

            seq.AppendCallback(() =>
            {
                thirdSound.clip = audioClip;
                thirdSound.loop = loop;
            });

            seq.Append(DOTween.To(x => thirdSound.volume = x, 0, volume, 0.5f))
                .AppendCallback(() => thirdSound.Play());

            seq.Play();
        }

        public void StopThirdSound(bool fadeOut = true)
        {
            if (!fadeOut)
            {
                thirdSound.Stop();    
            }
            
            DOTween.Sequence()
                .Append(DOTween.To(x => thirdSound.volume = x, thirdSound.volume, 0, 0.5f))
                .OnComplete(() => thirdSound.Stop())
                .Play();
        }
    }
}