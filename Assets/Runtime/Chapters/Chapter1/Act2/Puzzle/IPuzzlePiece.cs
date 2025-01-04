using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Act2.Puzzle
{
    public interface IPuzzlePiece
    {
        PuzzleBoard Board { get; set; }
        Transform Transform { get; }
        IPuzzlePiece Up { get; set; }
        IPuzzlePiece Left { get; set; }
        IPuzzlePiece Down { get; set; }
        IPuzzlePiece Right { get; set; }
        void Initialize();
        bool CanConnect(IPuzzlePiece target);
        List<IPuzzlePiece> ConnectedPieces { get; }
        Dictionary<IPuzzlePiece, Vector2> LinkDistanceMap { get; }
    }
}