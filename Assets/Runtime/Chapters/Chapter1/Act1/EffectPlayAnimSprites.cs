using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectPlayAnimSprites : IEffectNode
    {
        [SerializeField] private AnimSprites anims;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            var seq = DOTween.Sequence().SetDelay(delay);

            seq.Append(anims.Play());
            
            return seq;
        }
    }
}