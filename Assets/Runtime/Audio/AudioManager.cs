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

        public void ChangeBgMusic(AudioClip music, float volume = 0.1f)
        {
            DOTween.Sequence().SetTarget(bgMusic.transform)
                .Append(DOTween.To(x => bgMusic.volume = x, bgMusic.volume, 0, 0.5f))
                .AppendCallback(() => bgMusic.clip = music)
                .AppendCallback(() => bgMusic.Play())
                .Append(DOTween.To(x => bgMusic.volume = x, 0, volume, 2f))
                .Play();
        }

        public void VolumeOffBgMusic()
        {
            DOTween.Sequence().SetTarget(bgMusic.transform)
                .Append(DOTween.To(x => bgMusic.volume = x, bgMusic.volume, 0, 1f))
                .Play();
        }

        public void SetBgMusicAndOn(AudioClip music, float volume = 0.1f, float delay = 0f)
        {
            DOTween.Sequence().SetTarget(bgMusic.transform)
                .AppendCallback(() => bgMusic.clip = music)
                .AppendCallback(() => bgMusic.Play())
                .AppendInterval(delay)
                .Append(DOTween.To(x => bgMusic.volume = x, 0, volume, 2f))
                .Play();
        }

        public void PlaySound(AudioClip audioClip, float volumeScale = 1f, bool restart = false)
        {
            if (!restart && sound.isPlaying) return;
            sound.volume = 0.8f * volumeScale;
            sound.PlayOneShot(audioClip);
        }

        public void StopSound()
        {
            sound.Stop();
        }

        public void PlayThirdSound(AudioClip audioClip, float volumeScale = 1f, bool fadeIn = true, bool loop = false)
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

            seq.Append(DOTween.To(x => thirdSound.volume = x, 0, 0.3f * volumeScale, 0.5f))
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

        public void StopThirdSoundImmediately()
        {
            thirdSound.Stop();
        }
        
        public void PlayThirdSoundRandomClip(AudioClip audioClip, float duration, float volumeScale = 1f)
        {
            var seq = DOTween.Sequence();
            
            if (thirdSound.isPlaying) seq.Append(DOTween.To(x => thirdSound.volume = x, thirdSound.volume, 0, 0.2f));
            
            seq.AppendCallback(() =>
            {
                thirdSound.clip = audioClip;
                thirdSound.time = Random.Range(0f, audioClip.length - duration);
                thirdSound.volume = 0.3f * volumeScale;
            })
            .AppendCallback(() =>
            {
                thirdSound.Play();
                Invoke(nameof(StopThirdSoundImmediately), duration);
            });

            seq.Play();
        }
    }
}