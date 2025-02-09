using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Chapters
{
    [Serializable]
    public class EffectCallback : IEffectNode
    {
        [SerializeField] private UnityEvent events;
        [SerializeField] private float delay = 0f;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .AppendCallback(() => events.Invoke());
        }
    }
}