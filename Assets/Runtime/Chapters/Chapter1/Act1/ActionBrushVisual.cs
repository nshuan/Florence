using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class ActionBrushVisual : MonoBehaviour
    {
        private void OnEnable()
        {
            ActionBrush.OnMove += OnMouseMove;
            ActionBrush.OnMouseUp += OnMouseUp;
        }

        private void OnDisable()
        {
            ActionBrush.OnMove -= OnMouseMove;
            ActionBrush.OnMouseUp -= OnMouseUp;
        }

        private void OnMouseMove()
        {
            transform.position = ActionBrush.Target.position;
        }

        private void OnMouseUp()
        {
            DoReturn();
        }

        private Tween DoReturn()
        {
            return transform.DOLocalMove(Vector3.zero, 0.2f);
        }
    }
}