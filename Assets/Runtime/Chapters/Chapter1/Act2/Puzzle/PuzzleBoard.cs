using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using Runtime.Core;
using Unity.Collections;
using UnityEngine;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class PuzzleBoard : MonoBehaviour
    {
        [SerializeField] private Vector2 pieceSize;
        
        private IPuzzlePiece[] pieces;
        public PuzzleHelper PieceGroupHelper { get; private set; } = new PuzzleHelper();

        private void Awake()
        {
            Initialize();
            ShuffleBoard();
        }

        private void Initialize()
        {
            pieces = GetComponentsInChildren<IPuzzlePiece>();
            foreach (var piece in pieces)
            {
                piece.Initialize();
                piece.Board = this;
            }
        }

        private void ShuffleBoard()
        {
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i].Transform.localPosition = initializePlaceholder[i].localPosition;
            }
        }

        public void Check(IPuzzlePiece pieceToCheck)
        {
            const float connectDuration = 0.2f;
            
            BlockUI.Instance.Block();
            
            var seq = DOTween.Sequence();

            var connectList = new List<IPuzzlePiece>();
            if (pieceToCheck.Up != null && pieceToCheck.CanConnect(pieceToCheck.Up))
            {
                var upDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                 pieceToCheck.LinkDistanceMap[pieceToCheck.Up] - 
                                 (Vector2)pieceToCheck.Up.Transform.localPosition;

                foreach (var piece in PieceGroupHelper.PieceGroup(pieceToCheck.Up))
                {
                    seq.Join(piece.Transform.DOLocalMove(upDistance, connectDuration).SetRelative());
                }
                
                connectList.Add(pieceToCheck.Up);
            }

            if (pieceToCheck.Left != null && pieceToCheck.CanConnect(pieceToCheck.Left))
            {
                var leftDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                 pieceToCheck.LinkDistanceMap[pieceToCheck.Left] - 
                                 (Vector2)pieceToCheck.Left.Transform.localPosition;

                foreach (var piece in PieceGroupHelper.PieceGroup(pieceToCheck.Left))
                {
                    seq.Join(piece.Transform.DOLocalMove(leftDistance, connectDuration).SetRelative());
                }
                
                connectList.Add(pieceToCheck.Left);
            }

            if (pieceToCheck.Down != null && pieceToCheck.CanConnect(pieceToCheck.Down))
            {
                var downDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                 pieceToCheck.LinkDistanceMap[pieceToCheck.Down] - 
                                 (Vector2)pieceToCheck.Down.Transform.localPosition;

                foreach (var piece in PieceGroupHelper.PieceGroup(pieceToCheck.Down))
                {
                    seq.Join(piece.Transform.DOLocalMove(downDistance, connectDuration).SetRelative());
                }
                
                connectList.Add(pieceToCheck.Down);
            }

            if (pieceToCheck.Right != null && pieceToCheck.CanConnect(pieceToCheck.Right))
            {
                var rightDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                   pieceToCheck.LinkDistanceMap[pieceToCheck.Right] - 
                                   (Vector2)pieceToCheck.Right.Transform.localPosition;

                foreach (var piece in PieceGroupHelper.PieceGroup(pieceToCheck.Right))
                {
                    seq.Join(piece.Transform.DOLocalMove(rightDistance, connectDuration).SetRelative());
                }

                connectList.Add(pieceToCheck.Right);
            }

            seq.Play().OnComplete(() =>
            {
                foreach (var linkPiece in connectList.Where(linkPiece => !pieceToCheck.ConnectedPieces.Contains(linkPiece)))
                {
                    pieceToCheck.ConnectedPieces.Add(linkPiece);
                    linkPiece.ConnectedPieces.Add(pieceToCheck);
                    PieceGroupHelper.ConnectGroup(pieceToCheck, linkPiece);
                }
                
                BlockUI.Instance.Unblock();
            });
        }

        #region Puzzle builder

        [SerializeField] private Transform[] initializePlaceholder;

        #endregion
    }
}