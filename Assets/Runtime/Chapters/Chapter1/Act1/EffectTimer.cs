using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectTimer : IEffectNode
    {
        [SerializeField] private AnimSprites timerAnims;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            var seq = DOTween.Sequence().SetDelay(delay);

            seq.Append(timerAnims.Play());
            
            return seq;
        }
    }
}