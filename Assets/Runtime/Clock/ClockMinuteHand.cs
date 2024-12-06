using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Runtime.Clock
{
    public class ClockMinuteHand : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private Transform pivot;
        
        public event Action<float> OnMove; // Invoke with moved angle in degree
        public event Action OnReset;
        
        private bool IsHolding { get; set; }
        private float _currentAngle = 360;

        private void Awake()
        {
            _currentAngle = 360;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!IsHolding) return;

            var direction = (Vector2)UnityEngine.Input.mousePosition - RectTransformUtility.WorldToScreenPoint(Camera.main, pivot.position);
            var rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rotateAngle = (rotateAngle + 360) % 360;
            var deltaAngle = Mathf.DeltaAngle(_currentAngle, rotateAngle);

            // Prevent reverse
            if (deltaAngle >= 0) return;
            
            // Check if the minute hand has finished a round
            var isReset = _currentAngle > 2 && (rotateAngle <= 2 || rotateAngle > _currentAngle);
            
            _currentAngle = rotateAngle;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, _currentAngle), Time.deltaTime * 120);
            
            OnMove?.Invoke(deltaAngle);
            if (isReset)
            {
                OnReset?.Invoke();
            }
        }
    }
}