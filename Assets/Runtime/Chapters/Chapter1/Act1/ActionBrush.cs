using System;
using DG.Tweening;
using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act1
{
    public class ActionBrush : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        private bool IsHolding { get; set; }
        private Vector3 _mouseAnchor;
        private Vector3 _targetAnchor;
        private Camera _mainCam;

        public static event Action OnMove;
        public static event Action OnMouseUp;
        
        private void Awake()
        {
            _mainCam = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill();
            IsHolding = true;
            _mouseAnchor = UnityEngine.Input.mousePosition;
            _targetAnchor = transform.localPosition;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (IsHolding)
            {
                var distance = UnityEngine.Input.mousePosition - _mouseAnchor;
                transform.localPosition = _targetAnchor + distance;
                OnMove?.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
            OnMouseUp?.Invoke();
            DoReturn();
        }

        private Tween DoReturn()
        {
            return transform.DOLocalMove(Vector3.zero, 0.2f);
        }
    }
}