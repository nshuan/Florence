using System;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Runtime.Chapters.Chapter2.Act1.LetterScene
{
    public class LetterSceneBook : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField] private LetterSceneBox targetBox;
        [SerializeField] private Transform targetBoxField;
        [SerializeField] private Vector2 targetBoxOffset;

        private Transform TargetBoxTransform => targetBoxField;
        private Vector2 targetBoxSize;
        private bool IsInBox
        {
            get
            {
                if (targetBoxSize.magnitude <= 0.001f)
                    targetBoxSize = TargetBoxTransform.GetComponent<RectTransform>().rect.size;
                return transform.localPosition.x > TargetBoxTransform.localPosition.x - targetBoxSize.x / 2
                       && transform.localPosition.x < TargetBoxTransform.localPosition.x + targetBoxSize.x / 2
                       && transform.localPosition.y > TargetBoxTransform.localPosition.y - targetBoxSize.y / 2
                       && transform.localPosition.y < TargetBoxTransform.localPosition.y + targetBoxSize.y / 2;
            }
        }
        
        private bool IsInBoxOffset
        {
            get
            {
                if (targetBoxSize.magnitude <= 0.001f)
                    targetBoxSize = TargetBoxTransform.GetComponent<RectTransform>().rect.size;
                return transform.localPosition.x > TargetBoxTransform.localPosition.x - targetBoxSize.x / 2 - targetBoxOffset.x
                       && transform.localPosition.x < TargetBoxTransform.localPosition.x + targetBoxSize.x / 2 + targetBoxOffset.x
                       && transform.localPosition.y > TargetBoxTransform.localPosition.y - targetBoxSize.y / 2 - targetBoxOffset.y
                       && transform.localPosition.y < TargetBoxTransform.localPosition.y + targetBoxSize.y / 2 + targetBoxOffset.y;
            }
        }

        private bool canMove = true;

        private Vector3 originalPos;
        
        private void Awake()
        {
            originalPos = transform.localPosition;
        }

        private Tween DoReturn()
        {
            return transform.DOLocalMove(originalPos, 0.5f);
        }

        private Tween DoClampPos()
        {
            var target = transform.localPosition;

            var distance = Mathf.Abs(transform.localPosition.x - TargetBoxTransform.localPosition.x);
            if (distance > targetBoxSize.x / 2)
                target = TargetBoxTransform.localPosition +
                         (transform.localPosition - TargetBoxTransform.localPosition) / distance * targetBoxSize.x / 2;
            else
            {
                distance = Mathf.Abs(transform.localPosition.y - TargetBoxTransform.localPosition.y);
                if (distance > targetBoxSize.y / 2)
                    target = TargetBoxTransform.localPosition +
                             (transform.localPosition - TargetBoxTransform.localPosition) / distance * targetBoxSize.y / 2;
            }

            return transform.DOLocalMove(target, 0.5f);
        }

        private bool isHolding;
        private Vector2 _mouseAnchor;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canMove) return;
            
            transform.DOKill();
            isHolding = true;
            _mouseAnchor = CameraUtility.ScreenToWorldPoint(eventData.position);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (isHolding)
            {
                var distance = CameraUtility.ScreenToWorldPoint(eventData.position) - (Vector3)_mouseAnchor;
                _mouseAnchor = CameraUtility.ScreenToWorldPoint(eventData.position);
                transform.position += distance;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
            if (!targetBox.IsOpen || !IsInBoxOffset)
                DoReturn();
            else
            {
                canMove = false;
                
                if (IsInBox)
                {
                    transform.SetParent(targetBox.transform);
                    return;
                }

                DoClampPos().OnComplete(() => transform.SetParent(targetBox.transform));
            }
        }
    }
}