using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class WritingArm : MonoBehaviour
    {
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;

        private Vector3 originalPos;
        
        private void OnEnable()
        {
            DoWrite().SetTarget(transform).Play();
        }

        private void OnDisable()
        {
            transform.DOKill();
            transform.position = originalPos;
        }

        private Tween DoWrite()
        {
            const float moveRange = 3f;
            const float moveTimeStep = 0.2f;
            
            originalPos = transform.position;
            
            var seq = DOTween.Sequence();
            
            var direction = (endPos.position - startPos.position).normalized;
            if (endPos.position.y < startPos.position.y)
            {
                direction = -direction;
                (endPos, startPos) = (startPos, endPos);
            }
            
            seq.AppendInterval(0.5f)
                .Append(transform.DOLocalMove(direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(-direction * moveRange, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(direction * moveRange, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(-direction * moveRange / 2, moveTimeStep).SetRelative());
            
            seq.Append(transform.DOMove(endPos.position, 0.5f))
                .AppendInterval(0.5f)
                .Append(transform.DOLocalMove(-direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(-direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(direction * moveRange / 2, moveTimeStep).SetRelative());
            
            seq.Append(transform.DOMove(startPos.position, 0.5f))
                .AppendInterval(0.5f)
                .Append(transform.DOLocalMove(direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(-direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(direction * moveRange / 2, moveTimeStep).SetRelative())
                .Append(transform.DOLocalMove(-direction * moveRange / 2, moveTimeStep).SetRelative());

            seq.Append(transform.DOMove(originalPos, 0.5f));
            
            seq.SetLoops(-1, LoopType.Restart);
            
            return null;
        }
    }
}