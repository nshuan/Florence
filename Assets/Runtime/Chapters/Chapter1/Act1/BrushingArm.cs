using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class BrushingArm : MonoBehaviour
    {
        [SerializeField] private Transform arm;
        [SerializeField] private Vector3 localPosStart;
        [SerializeField] private Vector3 localPosEnd;
        [SerializeField] private int speedScale;

        private bool _isForward;
        private Vector3 _forwardDirection;
        private float _defaultSpeed;
        private float Speed => _defaultSpeed * speedScale;
        
        private void Awake()
        {
            arm.localPosition = localPosStart;
            _isForward = true;
            _forwardDirection = (localPosEnd - localPosStart).normalized;
            _defaultSpeed = 1f;
            ActionBrush.OnMove += OnBrush;
            ActionBrush.OnMouseUp += OnPauseBrush;
        }

        private void OnDestroy()
        {
            ActionBrush.OnMove -= OnBrush;
            ActionBrush.OnMouseUp -= OnPauseBrush;
        }

        private void OnBrush()
        {
            UpdateArm();
        }

        private void OnPauseBrush()
        {
            _isForward = true;
            arm.DOLocalMove(localPosStart, 0.2f);
        }

        private void UpdateArm()
        {
            if (_isForward)
            {
                arm.localPosition += _forwardDirection * Speed;
                if (Vector2.Distance(localPosStart, localPosEnd) <= Vector2.Distance(arm.localPosition, localPosEnd))
                    _isForward = false;
            }
            else
            {
                arm.localPosition -= _forwardDirection * Speed;
                if (Vector2.Distance(localPosEnd, localPosStart) <= Vector2.Distance(arm.localPosition, localPosEnd))
                    _isForward = true;
            }
        }
    }
}