using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Audio
{
    public class AudioPlayOnSlide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private AudioPlay audioPlay;
        [SerializeField] private float volumeScale = 1f;

        private bool isHolding = false;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            audioPlay.Stop();
            isHolding = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (isHolding)
                audioPlay.Play(volumeScale);
        }
    }
}