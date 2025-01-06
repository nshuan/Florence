using System;
using DG.Tweening;
using Runtime.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2.Stamp
{
    public class Stamp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        private static Vector3 stampVector = new Vector3(27, -28);
        private static float stampRotate = -18f;
        private static Vector3 inkOffset = new Vector3(72, -38);
        
        [SerializeField] private PaperStack paperStack;
        
        private bool IsHolding { get; set; }
        private Vector3 _mouseAnchor;
        private Vector3 _targetAnchor;
        private Vector3 initialPos;

        private void Awake()
        {
            initialPos = transform.localPosition;
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
            }
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
            BlockUI.Instance.Block();
            if (paperStack.IsStampable(transform.localPosition + inkOffset))
            {
                DoStampAndReturn().OnComplete(BlockUI.Instance.Unblock);
            }
            else
            {
                DoReturn().OnComplete(BlockUI.Instance.Unblock);
            }
        }

        private Tween DoReturn()
        {
            return transform.DOLocalMove(initialPos, 0.5f);
        }

        private Tween DoStampAndReturn()
        {
            var targetStampPos = transform.localPosition + stampVector;
            var targetInpPos = transform.localPosition + inkOffset;

            return DOTween.Sequence()
                .Join(transform.DORotate(new Vector3(0f, 0f, stampRotate), 0.4f))
                .Join(transform.DOLocalMove(targetStampPos, 0.4f))
                .AppendCallback(() => paperStack.Stamp(targetInpPos))
                .Append(transform.DOLocalMoveY(100f, 0.3f).SetRelative())
                .Join(transform.DORotate(new Vector3(0f, 0f, 0f), 0.3f))
                .Append(DoReturn())
                .Append(paperStack.DoNextPaper());



























































































































































































































        }
    }
}