using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectPunchScale : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private float strength = 0.1f;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay = 0f;
        
        public Tween GetTween()
        {
            var seq = DOTween.Sequence().SetDelay(delay);

            seq.Append(target.DOPunchScale(strength * Vector3.one, duration));
            
            return seq;
        }
    }
}