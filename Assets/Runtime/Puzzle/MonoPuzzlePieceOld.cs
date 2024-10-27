using System;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Puzzle
{
    public class MonoPuzzlePieceOld : MonoBehaviour
    {
        [SerializeField] private Vector2Int coordinate;
        public Vector2Int Coordinate => coordinate;
        public IPuzzlePiece UpPiece { get; set; }
        public IPuzzlePiece RightPiece { get; set; }
        public IPuzzlePiece DownPiece { get; set; }
        public IPuzzlePiece LeftPiece { get; set; }
        
        public IPuzzlePiece HeadPiece { get; set; }
        public IPuzzlePiece TailPiece { get; set; }


        public bool IsDraggable { get; set; } = true;
        public bool IsMouseDragging { get; set; }
        
        public void Check()
        {
            if (UpPiece != null && _connectStatus.x == 0)
            {
                // if (Mathf.Abs(UpPiece.LocalPosition.x - LocalPosition.x) > SafeDistance) return;
                // if (UpPiece.LocalPosition.y - LocalPosition.y < 0 ||
                //     UpPiece.LocalPosition.y - LocalPosition.y > SafeDistance + 100) return;
                
            }
        }

        private Vector4 _connectStatus = Vector4.zero;
        public Tween DoMovePiece(Vector3 targetLocal)
        {
            IsDraggable = false;

            var seq = DOTween.Sequence()
                .Append(transform.DOLocalMove(targetLocal, 0.3f).SetEase(Ease.Unset));

            // if (UpPiece is IDraggable { IsDraggable: true })
            //     seq.Join(UpPiece.DoMovePiece(targetLocal + 100f * Vector3.up));
            // if (RightPiece is IDraggable { IsDraggable: true })
            //     seq.Join(RightPiece.DoMovePiece(targetLocal + 100f * Vector3.right));
            // if (DownPiece is IDraggable { IsDraggable: true })
            //     seq.Join(DownPiece.DoMovePiece(targetLocal + 100f * Vector3.down));
            // if (LeftPiece is IDraggable { IsDraggable: true })
                // seq.Join(LeftPiece.DoMovePiece(targetLocal + 100f * Vector3.left));

            return seq.OnComplete(() =>
            {
                IsDraggable = true;
            });
        }

        public void MouseDrag(Vector3 target, bool isTargetLocal = false)
        {
            if (isTargetLocal)
                transform.localPosition = target;
            else
                transform.position = target;
            
            TailPiece?.MouseDrag(LocalPosition + (HeadPiece.Coordinate - Coordinate) * 100, true);
        }

        public Transform Transform => transform;
        public Vector2 LocalPosition => transform.localPosition;

        private void Update()
        {
            if (IsMouseDragging)
            {
                MouseDrag(CameraUtility.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsMouseDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsMouseDragging = false;
        }
    }
}