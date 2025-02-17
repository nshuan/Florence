using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Act3
{
    public class EffectPlane : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Vector3 originalPos;

        private void Start()
        {
            originalPos = target.localPosition;
        }

        private void OnEnable()
        {
            transform.DOKill();
            DoEffect().Play();
        }

        private Tween DoEffect()
        {
            var seq = DOTween.Sequence().SetTarget(transform);

            seq.Append(target.DOLocalMoveY(6f, 2f).SetEase(Ease.InOutSine).SetRelative())
                .AppendInterval(0.5f)
                .Append(target.DOLocalMoveY(-12f, 3f).SetEase(Ease.InOutSine).SetRelative())
                .AppendInterval(0.5f)
                .Append(target.DOLocalMoveY(6f, 1f).SetEase(Ease.InOutSine).SetRelative());

            seq.SetLoops(-1);
            
            return seq;
        }
    }
}