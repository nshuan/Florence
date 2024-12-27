using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectMove : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform targetPos;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .Append(target.DOMove(targetPos.position, duration));
        }
    }
}