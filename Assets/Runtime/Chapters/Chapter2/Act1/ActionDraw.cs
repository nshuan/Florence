using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Chapter2.Act1
{
    public class ActionDraw : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Awake()
        {
            if (target == null) target = transform;
        }

        private void OnEnable()
        {
            target.DOKill();
            DoEffect().Play();
        }

        private void OnDestroy()
        {
            target.DOKill();
        }

        private Tween DoEffect()
        {
            var seq = DOTween.Sequence()
                .SetTarget(target)
                .SetLoops(-1, LoopType.Yoyo)
                .SetDelay(1.2f);

            // 5 - 1,2 - 4,2 - 3,4
            seq.Append(target.DORotate(new Vector3(0f, 0f, 5f), 0.2f))
                .Append(target.DORotate(new Vector3(0f, 0f, 1.2f), 0.12f))
                .Append(target.DORotate(new Vector3(0f, 0f, 4.2f), 0.16f))
                .Append(target.DORotate(new Vector3(0f, 0f, 2.6f), 0.08f))
                .Append(target.DORotate(new Vector3(0f, 0f, 3.2f), 0.05f));
            
            return seq;
        }
    }
}