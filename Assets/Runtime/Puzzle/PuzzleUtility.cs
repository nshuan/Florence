using DG.Tweening;
using UnityEngine;

namespace Runtime.Puzzle
{
    public static class PuzzleUtility
    {
        private const float SafeDistance = 50f;
        private const int PieceGap = 100;
        
        public static bool IsConnectable(IPuzzlePiece piece1, IPuzzlePiece piece2)
        {
            if (piece1.Coordinate == piece2.Up)
            {
                if (Mathf.Abs(piece1.LocalPosition.x - piece2.LocalPosition.x) > SafeDistance) return false;
                if (piece1.LocalPosition.y - piece2.LocalPosition.y < 0 ||
                    piece1.LocalPosition.y - piece2.LocalPosition.y > SafeDistance + 100) return false;

                return true;
            }
            if (piece1.Coordinate == piece2.Right)
            {
                if (Mathf.Abs(piece1.LocalPosition.y - piece2.LocalPosition.y) > SafeDistance) return false;
                if (piece1.LocalPosition.x - piece2.LocalPosition.x < 0 ||
                    piece1.LocalPosition.x - piece2.LocalPosition.x > SafeDistance + 100) return false;

                return true;
            }
            if (piece1.Coordinate == piece2.Down)
            {
                if (Mathf.Abs(piece1.LocalPosition.x - piece2.LocalPosition.x) > SafeDistance) return false;
                if (piece1.LocalPosition.y - piece2.LocalPosition.y > 0 ||
                    piece1.LocalPosition.y - piece2.LocalPosition.y < - SafeDistance - 100) return false;

                return true;
            }
            if (piece1.Coordinate == piece2.Left)
            {
                if (Mathf.Abs(piece1.LocalPosition.y - piece2.LocalPosition.y) > SafeDistance) return false;
                if (piece1.LocalPosition.x - piece2.LocalPosition.x > 0 ||
                    piece1.LocalPosition.x - piece2.LocalPosition.x < - SafeDistance - 100) return false;

                return true;
            }

            return false;
        }

        public static Tween DoConnect(IPuzzlePiece connectThis, IPuzzlePiece connectWith, Vector2Int direction)
        {
            return connectThis.Transform.DOLocalMove(connectThis.LocalPosition + PieceGap * direction, 0.3f)
                .SetEase(Ease.Unset);
        }

        public static Tween DoMoveWith(this IPuzzlePiece moveThis, IPuzzlePiece withThis)
        {
            return DOTween.Sequence();
            // return moveThis.Transform.DOLocalMove(withThis.LocalPosition + PieceGap * direction, 0.3f)
            //     .SetEase(Ease.Unset);
        }
    }
}