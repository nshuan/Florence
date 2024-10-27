using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Puzzle
{
    public interface IPuzzlePiece : ICoordinate, IDraggable
    {
        Transform Transform { get; }
        Vector2 LocalPosition { get; }
        IPuzzlePiece UpPiece { get; set; }
        IPuzzlePiece RightPiece { get; set; }
        IPuzzlePiece DownPiece { get; set; }
        IPuzzlePiece LeftPiece { get; set; }
        List<IPuzzlePiece> ConnectedPieces { get; set; }
        void Check();
        Tween DoConnect(IPuzzlePiece connectWith);
    }

    public interface IDraggable : IPointerDownHandler, IPointerUpHandler
    {
        bool IsReleased { get; set; }
        void MouseDrag(Vector3 target, bool isTargetLocal = false);
        void RegisterOnHeld(Action<IPuzzlePiece> onHeld);
        void RegisterOnReleased(Action<IPuzzlePiece> onReleased);
    }

    public interface ICoordinate
    {
        Vector2Int Coordinate { get; }
        Vector2Int Up => Coordinate + new Vector2Int(0, 1);
        Vector2Int Right => Coordinate + new Vector2Int(1, 0);
        Vector2Int Down => Coordinate + new Vector2Int(0, -1);
        Vector2Int Left => Coordinate + new Vector2Int(-1, 0);
    }
}