using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class EffectFloatingOut : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 boardSize;

        private void Awake()
        {
            boardSize = transform.parent.GetComponent<RectTransform>().rect.size - new Vector2(250, 250);
        }

        private void OnEnable()
        {
            CheckCoordinateAndFloatOut();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOKill();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CheckCoordinateAndFloatOut();
        }

        private void CheckCoordinateAndFloatOut()
        {
            var direction = transform.localPosition.normalized;
            Vector3 target;
            if (direction.x == 0 || Mathf.Abs(boardSize.x / direction.x * direction.y) > boardSize.y)
                target = Mathf.Abs(boardSize.y / 2 / direction.y) * direction;
            else
                target = Mathf.Abs(boardSize.x / 2 / direction.x) * direction;

            var seq = DOTween.Sequence().SetTarget(transform);

            seq.Append(transform.DOLocalMove(target, Random.Range(19f, 20f)).SetEase(Ease.Linear));
            
            seq.Play();
        }
    }
}