using System;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2.Cleaning
{
    public class CleaningItem : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        public CleaningBox Box { get; set; }
        private bool canMove = true;
        private Vector2 originalPos;
        private int originalIndex;

        private void Awake()
        {
            originalPos = transform.localPosition;
        }

        private bool CanPutInBox()
        {
            return transform.localPosition.x > -Box.BoxSize.x / 2
                   && transform.localPosition.x < Box.BoxSize.x / 2
                   && transform.localPosition.y > -Box.BoxSize.y / 2
                   && transform.localPosition.y < Box.BoxSize.y / 2;
        }
        
        private bool isHolding;
        private Vector2 _mouseAnchor;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canMove) return;

            originalIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
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
            if (CanPutInBox())
            {
                canMove = false;
                Box.PutInBox(this);
            }
            else
            {
                transform.DOLocalMove(originalPos, 0.5f)
                    .OnComplete(() => transform.SetSiblingIndex(originalIndex));
            }
        }
    }
}