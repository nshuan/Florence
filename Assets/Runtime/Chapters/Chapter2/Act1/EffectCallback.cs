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
        
        public Tween GetTween()
        {
            return DOTween.Sequence()
                .AppendCallback(() => events.Invoke());
        }
    }
}