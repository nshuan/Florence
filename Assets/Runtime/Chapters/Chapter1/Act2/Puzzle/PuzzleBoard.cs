using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using Runtime.Audio;
using Runtime.Core;
using Runtime.Effects;
using Unity.Collections;
using UnityEngine;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class PuzzleBoard : MonoBehaviour, IProgress
    {
        private const float PieceMoveDuration = 0.2f;
        
        [SerializeField] private Vector2 pieceSize;
        [SerializeField] private AudioPlay connectAudio;
        [SerializeField] private bool moveToPositionOnComplete = true;

        public Action OnComplete { get; set; }
        private Vector3 firstPieceTargetPosition;
        private IPuzzlePiece[] pieces;
        public PuzzleHelper PieceGroupHelper { get; private set; } = new PuzzleHelper();

        private bool IsComplete =>
            PieceGroupHelper.PiecesInGroupCount == pieces.Length && PieceGroupHelper.GroupCount == 1;
        
        private void Awake()
        {
            Initialize();
            ShuffleBoard();
        }

        private void Initialize()
        {
            pieces = GetComponentsInChildren<IPuzzlePiece>();
            firstPieceTargetPosition = pieces[0].Transform.localPosition;
            foreach (var piece in pieces)
            {
                piece.Initialize();
                piece.Board = this;
            }
        }

        private void ShuffleBoard()
        {
            for (var i = initializePlaceholder.Length - 1; i > 0; i--)
            {
                // Get a random index
                var randomIndex = UnityEngine.Random.Range(0, i + 1);

                // Swap elements
                (initializePlaceholder[i], initializePlaceholder[randomIndex]) = (initializePlaceholder[randomIndex], initializePlaceholder[i]);
            }
            
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i].Transform.localPosition = initializePlaceholder[i].localPosition;
            }
        }

        public void Check(IPuzzlePiece checkPiece)
        {
            if (IsComplete) return;
            
            var groupToCheck = PieceGroupHelper.PieceGroup(checkPiece);
                
            BlockUI.Instance.Block();
            
            var seq = DOTween.Sequence();

            var connectedGroups = new List<IPuzzlePiece[]>();

            foreach (var pieceToCheck in groupToCheck)
            {
                if (pieceToCheck.Up != null && pieceToCheck.CanConnect(pieceToCheck.Up))
                {
                    var upDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                     pieceToCheck.LinkDistanceMap[pieceToCheck.Up] - 
                                     (Vector2)pieceToCheck.Up.Transform.localPosition;

                    var group = PieceGroupHelper.PieceGroup(pieceToCheck.Up);
                    
                    if (!group.Contains(pieceToCheck) && !connectedGroups.Contains(group))
                    {
                        foreach (var piece in group)
                        {
                            seq.Join(piece.Transform.DOLocalMove(upDistance, PieceMoveDuration).SetRelative());
                        }
                        
                        connectedGroups.Add(group);
                    }  
                }

                if (pieceToCheck.Left != null && pieceToCheck.CanConnect(pieceToCheck.Left))
                {
                    var leftDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                     pieceToCheck.LinkDistanceMap[pieceToCheck.Left] - 
                                     (Vector2)pieceToCheck.Left.Transform.localPosition;

                    var group = PieceGroupHelper.PieceGroup(pieceToCheck.Left);

                    if (!group.Contains(pieceToCheck) && !connectedGroups.Contains(group))
                    {
                        foreach (var piece in group)
                        {
                            seq.Join(piece.Transform.DOLocalMove(leftDistance, PieceMoveDuration).SetRelative());
                        }
                        
                        connectedGroups.Add(group);
                    }
                }

                if (pieceToCheck.Down != null && pieceToCheck.CanConnect(pieceToCheck.Down))
                {
                    var downDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                     pieceToCheck.LinkDistanceMap[pieceToCheck.Down] - 
                                     (Vector2)pieceToCheck.Down.Transform.localPosition;

                    var group = PieceGroupHelper.PieceGroup(pieceToCheck.Down);

                    if (!group.Contains(pieceToCheck) && !connectedGroups.Contains(group))
                    {
                        foreach (var piece in group)
                        {
                            seq.Join(piece.Transform.DOLocalMove(downDistance, PieceMoveDuration).SetRelative());
                        }
                        
                        connectedGroups.Add(group);
                    }
                }

                if (pieceToCheck.Right != null && pieceToCheck.CanConnect(pieceToCheck.Right))
                {
                    var rightDistance = (Vector2)pieceToCheck.Transform.localPosition +
                                       pieceToCheck.LinkDistanceMap[pieceToCheck.Right] - 
                                       (Vector2)pieceToCheck.Right.Transform.localPosition;

                    var group = PieceGroupHelper.PieceGroup(pieceToCheck.Right);
                    
                    if (!group.Contains(pieceToCheck) && !connectedGroups.Contains(group))
                    {
                        foreach (var piece in group)
                        {
                            seq.Join(piece.Transform.DOLocalMove(rightDistance, PieceMoveDuration).SetRelative());
                        }
                        
                        connectedGroups.Add(group);
                    }
                }
            }

            foreach (var group in connectedGroups.Where(group => group.Length > 0))
            {
                PieceGroupHelper.ConnectGroup(checkPiece, group[0]);
            }
            
            connectAudio.Play(0.5f);
            
            seq.Play().OnComplete(() =>
            {
                if (IsComplete)
                {
                    if (moveToPositionOnComplete)
                    {
                        foreach (var piece in pieces)
                        {
                            piece.Disable();
                        }
                        DoComplete(1f).Play().OnComplete(() => OnComplete?.Invoke());
                    }
                    else
                        OnComplete?.Invoke();
                }
                
                BlockUI.Instance.Unblock();
            });
        }

        public Tween DoComplete(float duration)
        {
            var seq = DOTween.Sequence(transform);
            
            var distance = firstPieceTargetPosition - pieces[0].Transform.localPosition;
            foreach (var piece in pieces)
            {
                seq.Join(piece.Transform.DOLocalMove(distance, 1f).SetRelative());
            }

            return seq;
        }

        #region Puzzle builder

        [SerializeField] private Transform[] initializePlaceholder;

        #endregion
    }
}