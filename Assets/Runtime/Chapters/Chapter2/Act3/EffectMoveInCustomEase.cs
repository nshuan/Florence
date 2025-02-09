using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act3
{
    [Serializable]
    public class EffectMoveInCustomEase : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform fromTargetPos;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private Ease ease = Ease.InOutSine;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .AppendCallback(() =>
                {
                    target.position = fromTargetPos.position;
                    target.gameObject.SetActive(true);
                })
                .Append(target.DOLocalMove(Vector3.zero, duration).SetEase(ease));
        }
    }
}