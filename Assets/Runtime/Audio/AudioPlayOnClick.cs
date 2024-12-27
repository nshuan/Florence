using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Audio
{
    public class AudioPlayOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float volume = 0.5f;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (audioClip == null) return;
            AudioManager.Instance.PlaySound(audioClip, volume, true);
        }
    }
}