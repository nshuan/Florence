using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class PuzzlePiece : MonoBehaviour, IPuzzlePiece, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField] private PuzzlePiece up;
        [SerializeField] private PuzzlePiece left;
        [SerializeField] private PuzzlePiece down;
        [SerializeField] private PuzzlePiece right;
        
        public PuzzleBoard Board { get; set; }
        public Transform Transform => transform;
        public IPuzzlePiece Up { get; set; }
        public IPuzzlePiece Left { get; set; }
        public IPuzzlePiece Down { get; set; }
        public IPuzzlePiece Right { get; set; }

        public Dictionary<IPuzzlePiece, Vector2> LinkDistanceMap { get; private set; }
        public List<IPuzzlePiece> ConnectedPieces { get; set; } = new List<IPuzzlePiece>();

        public void Initialize()
        {
            Up = up;
            Left = left;
            Down = down;
            Right = right;

            LinkDistanceMap = new Dictionary<IPuzzlePiece, Vector2>();
            if (Up is not null) LinkDistanceMap.Add(Up, Up.Transform.localPosition - transform.localPosition);
            if (Left is not null) LinkDistanceMap.Add(Left, Left.Transform.localPosition - transform.localPosition);
            if (Down is not null) LinkDistanceMap.Add(Down, Down.Transform.localPosition - transform.localPosition);
            if (Right is not null) LinkDistanceMap.Add(Right, Right.Transform.localPosition - transform.localPosition);

            ConnectedPieces = new List<IPuzzlePiece>();
        }

        public bool CanConnect(IPuzzlePiece target)
        {
            const float maxDelta = 30f;
            
            if (!LinkDistanceMap.ContainsKey(target)) return false;

            return ((Vector2)transform.localPosition + LinkDistanceMap[target] -
                    (Vector2)target.Transform.localPosition).magnitude <= maxDelta;
        }

        private bool isHolding = false;
        private Vector2 _mouseAnchor;
        private Vector2 _pieceAnchor;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill();
            isHolding = true;
            transform.SetAsLastSibling();
            _mouseAnchor = UnityEngine.Input.mousePosition;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (isHolding)
            {
                var distance = (Vector2)UnityEngine.Input.mousePosition - _mouseAnchor;
                _mouseAnchor = (Vector2)UnityEngine.Input.mousePosition;

                foreach (var piece in Board.PieceGroupHelper.PieceGroup(this))
                {
                    piece.Transform.SetAsLastSibling();
                    piece.Transform.localPosition = (Vector2)piece.Transform.localPosition + distance;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
            Board.Check(this);
        }
    }
}