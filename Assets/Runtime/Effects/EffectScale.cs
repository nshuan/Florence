using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectScale : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 targetScale = Vector3.one;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private Ease ease = Ease.Unset;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay).SetEase(ease)
                .Append(target.DOScale(targetScale, duration));
        }
    }
}