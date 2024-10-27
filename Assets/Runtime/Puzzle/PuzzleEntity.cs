using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Puzzle
{
    public class PuzzleEntity : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private List<MonoPuzzlePiece> pieces;

        private const float SafeDistance = 50f;

        private List<IPuzzlePiece> _piecesInHand;
        
        private void Awake()
        {
            // Init();
            _piecesInHand = new List<IPuzzlePiece>();
        }

        private void Init()
        {
            if (pieces == null) return;
            
            // Sort pieces by coordinate
            pieces.Sort((a, b) =>
            {
                // Compare by x, and if x is the same, compare by y
                var xComparison = a.Coordinate.x.CompareTo(b.Coordinate.x);
                return xComparison == 0 ? a.Coordinate.y.CompareTo(b.Coordinate.y) : xComparison;
            });
            
            var count = pieces.Count;
            foreach (var piece in pieces)
            {
                var index = CoordToIndex(((IPuzzlePiece)piece).Up);
                piece.UpPiece = index < 0 || index >= count ? null : pieces[index];
                index = CoordToIndex(((IPuzzlePiece)piece).Right);
                piece.RightPiece = index < 0 || index >= count ? null : pieces[index];
                index = CoordToIndex(((IPuzzlePiece)piece).Down);
                piece.DownPiece = index < 0 || index >= count ? null : pieces[index];
                index = CoordToIndex(((IPuzzlePiece)piece).Left);
                piece.LeftPiece = index < 0 || index >= count ? null : pieces[index];
                
                piece.RegisterOnHeld(GetHoldingPieces);
                piece.RegisterOnReleased(ReleasePieces);
            }
        }

        private void GetHoldingPieces(IPuzzlePiece pressedPiece)
        {
            GetLinkPiecesBfs(_piecesInHand, pressedPiece);
        }

        private void GetLinkPiecesBfs(List<IPuzzlePiece> piecesList, IPuzzlePiece rootPiece)
        {
            if (!rootPiece.IsReleased) return;
            
            piecesList.Add(rootPiece);
            
            if (rootPiece.ConnectedPieces is null || rootPiece.ConnectedPieces.Count <= 0) return;

            foreach (var piece in rootPiece.ConnectedPieces)
            {
                GetLinkPiecesBfs(piecesList, piece);
            }
        }

        private void ReleasePieces(IPuzzlePiece pressedPiece)
        {
            foreach (var piece in _piecesInHand)
            {
                piece.Check();
            }

            foreach (var piece in _piecesInHand)
            {
                piece.IsReleased = true;
            }
            
            _piecesInHand.Clear();
        }

        protected virtual void CheckAndConnectPiece(IPuzzlePiece pieceToConnect, IPuzzlePiece connectWith)
        {
            if (pieceToConnect.Coordinate == connectWith.Up)
            {
                if (Mathf.Abs(pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x) > SafeDistance) return;
                if (pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y < 0 ||
                    pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y > SafeDistance + 100) return;

                // pieceToConnect.DoMovePiece(connectWith.LocalPosition + 100f * Vector2.up).Play();
                connectWith.UpPiece = pieceToConnect;
                pieceToConnect.DownPiece = connectWith;
            }
            if (pieceToConnect.Coordinate == connectWith.Right)
            {
                if (Mathf.Abs(pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y) > SafeDistance) return;
                if (pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x < 0 ||
                    pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x > SafeDistance + 100) return;

                // pieceToConnect.DoMovePiece(connectWith.LocalPosition + 100f * Vector2.right).Play();
                connectWith.RightPiece = pieceToConnect;
                pieceToConnect.LeftPiece = connectWith;
            }
            if (pieceToConnect.Coordinate == connectWith.Down)
            {
                if (Mathf.Abs(pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x) > SafeDistance) return;
                if (pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y > 0 ||
                    pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y < - SafeDistance - 100) return;

                // pieceToConnect.DoMovePiece(connectWith.LocalPosition + 100f * Vector2.down).Play();
                connectWith.DownPiece = pieceToConnect;
                pieceToConnect.UpPiece = connectWith;
            }
            if (pieceToConnect.Coordinate == connectWith.Left)
            {
                if (Mathf.Abs(pieceToConnect.LocalPosition.y - connectWith.LocalPosition.y) > SafeDistance) return;
                if (pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x > 0 ||
                    pieceToConnect.LocalPosition.x - connectWith.LocalPosition.x < - SafeDistance - 100) return;

                // pieceToConnect.DoMovePiece(connectWith.LocalPosition + 100f * Vector2.left).Play();
                connectWith.LeftPiece = pieceToConnect;
                pieceToConnect.RightPiece = connectWith;
            }
        }

        private int CoordToIndex(Vector2Int coordinate)
        {
            return coordinate.y * size.x + coordinate.x;
        }
    }
}