using Runtime.Audio;
using UnityEngine;

namespace Runtime.Chapters.Act2.Alarm
{
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private AudioPlay audioPlay;
        [SerializeField] private float volumeScale = 1f;
        [SerializeField] private bool loop = true;

        public void OnEnable()
        {
            AudioManager.Instance.PlayThirdSound(audioPlay.audioClip, volumeScale, true, loop);
        }
    }
}