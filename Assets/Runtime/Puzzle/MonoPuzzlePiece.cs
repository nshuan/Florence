using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Puzzle
{
    public class MonoPuzzlePiece : MonoBehaviour, IPuzzlePiece
    {
        [SerializeField] private Vector2Int coordinate;
        public Vector2Int Coordinate => coordinate;

        public bool IsReleased { get; set; }
        public void MouseDrag(Vector3 target, bool isTargetLocal = false)
        {
            
        }

        private Action<IPuzzlePiece> _onHeld;
        private Action<IPuzzlePiece> _onReleased;
        public void RegisterOnHeld(Action<IPuzzlePiece> onHeld)
        {
            _onHeld += onHeld;
        }

        public void RegisterOnReleased(Action<IPuzzlePiece> onReleased)
        {
            _onReleased += onReleased;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _onHeld?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _onReleased?.Invoke(this);
        }

        public Transform Transform => transform;
        public Vector2 LocalPosition => transform.localPosition;
        public IPuzzlePiece UpPiece { get; set; }
        public IPuzzlePiece RightPiece { get; set; }
        public IPuzzlePiece DownPiece { get; set; }
        public IPuzzlePiece LeftPiece { get; set; }
        public List<IPuzzlePiece> ConnectedPieces { get; set; }
        public void Check()
        {
            if (UpPiece.IsReleased && PuzzleUtility.IsConnectable(this, UpPiece))
            {
                if (!ConnectedPieces.Contains(UpPiece))
                {
                    ConnectedPieces.Add(UpPiece);
                    // UpPiece.do
                }
            }

            if (RightPiece.IsReleased && PuzzleUtility.IsConnectable(this, RightPiece))
            {
                if (!ConnectedPieces.Contains(RightPiece)) ConnectedPieces.Add(RightPiece);
            }

            if (DownPiece.IsReleased && PuzzleUtility.IsConnectable(this, DownPiece))
            {
                if (!ConnectedPieces.Contains(DownPiece)) ConnectedPieces.Add(DownPiece);
            }

            if (LeftPiece.IsReleased && PuzzleUtility.IsConnectable(this, LeftPiece))
            {
                if (!ConnectedPieces.Contains(LeftPiece)) ConnectedPieces.Add(LeftPiece);
            }
        }

        public Tween DoConnect(IPuzzlePiece connectWith)
        {
            var seq = DOTween.Sequence()
                .Append(transform.DOLocalMove(connectWith.LocalPosition + 100 * Vector2Int.up , 0.3f).SetEase(Ease.Unset));


            return seq;
        }
    }
}