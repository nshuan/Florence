using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Audio
{
    public class AudioPlayOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip audioClip;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (audioClip == null) return;
            AudioManager.Instance.PlaySound(audioClip);
        }
    }
}