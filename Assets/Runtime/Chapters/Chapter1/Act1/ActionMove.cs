using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Runtime.Chapters.Act1
{
    public class ActionMove : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform target;
        [SerializeField] protected MoveDirection direction = MoveDirection.Left;
        [SerializeField] protected int distance = 1000;
        [SerializeField] protected float duration = 0.5f;

        protected Vector2Int DirectionVector => DirectionToVector(direction);
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            DoAction();
        }

        protected Tween DoAction()
        {
            var direction3 = new Vector3(DirectionVector.x, DirectionVector.y, 0);
            return target.DOLocalMove(direction3 * distance, duration).SetRelative();
        }

        private Vector2Int DirectionToVector(MoveDirection moveDirection)
        {
            return moveDirection switch
            {
                MoveDirection.Up => Vector2Int.up,
                MoveDirection.Right => Vector2Int.right,
                MoveDirection.Down => Vector2Int.down,
                MoveDirection.Left => Vector2Int.left,
                _ => Vector2Int.left
            };
        }
        
        protected enum MoveDirection
        {
            Up,
            Right,
            Down,
            Left
        }
    }
}