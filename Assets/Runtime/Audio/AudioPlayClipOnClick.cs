using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Audio
{
    public class AudioPlayClipOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float duration;
        [SerializeField] private float volumeScale = 1f;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (audioClip == null) return;
            AudioManager.Instance.PlayThirdSoundRandomClip(audioClip, duration, volumeScale);
        }
    }
}