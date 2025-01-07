using System;
using Core;
using DG.Tweening;
using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act1
{
    public class ActionBrush : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField] private Vector3 originalPosition = Vector3.zero;
        
        private bool IsHolding { get; set; }
        private Vector3 _mouseAnchor;
        private Vector3 _targetAnchor;
        private Camera _mainCam;

        public static event Action OnMove;
        public static event Action OnMouseUp;
        public static Transform Target;
        
        private void Awake()
        {
            _mainCam = Camera.main;
            Target = transform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill();
            IsHolding = true;
            _mouseAnchor = CameraUtility.ScreenToWorldPoint(eventData.position);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (IsHolding)
            {
                var distance = CameraUtility.ScreenToWorldPoint(eventData.position) - (Vector3)_mouseAnchor;
                _mouseAnchor = CameraUtility.ScreenToWorldPoint(eventData.position);
                transform.position += distance;
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
            return transform.DOLocalMove(originalPosition, 0.2f);
        }
    }
}