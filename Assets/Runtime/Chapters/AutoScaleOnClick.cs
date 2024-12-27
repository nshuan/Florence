using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters
{
    public class AutoScaleOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform scaleTarget;
        [Range(0f, 1f)] [SerializeField] private float duration = 0.2f;
        [SerializeField] private Vector3 start = Vector3.one, end = Vector3.one * 0.95f;

        private void OnValidate()
        {
            if (scaleTarget == null) scaleTarget = transform;
        }

        public void OnPointerDown(PointerEventData eventData) {
            scaleTarget.DOKill();
            scaleTarget.DOScale(end, duration);        
        }

        public void OnPointerUp(PointerEventData eventData) {
            scaleTarget.DOKill();
            scaleTarget.DOScale(start, duration);
        }

        private void OnDisable()
        {
            scaleTarget.DOKill();
            scaleTarget.localScale = start;
        }
    }
}