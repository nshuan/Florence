using Runtime.Audio;
using UnityEngine;

namespace Runtime.Core
{
    public class InGameBackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip bgMusic;
        
        private void Start()
        {
            AudioManager.Instance.SetBgMusicAndOn(bgMusic);
        }
    }
}