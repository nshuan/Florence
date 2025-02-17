using UnityEngine;

namespace Runtime.Audio
{
    public class BgMusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip music;
        [SerializeField] private float volume;
        [SerializeField] private bool isAlreadyOff = true;

        public void Play()
        {
            if (isAlreadyOff)
                AudioManager.Instance.SetBgMusicAndOn(music, volume);
            else
                AudioManager.Instance.ChangeBgMusic(music, volume);
        }

        public void Play(float delay)
        {
            if (isAlreadyOff)
                AudioManager.Instance.SetBgMusicAndOn(music, volume, delay);
            else
                AudioManager.Instance.ChangeBgMusic(music, volume);
        }
    }
}