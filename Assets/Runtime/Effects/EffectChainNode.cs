using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectChainNode : IEffectNode
    {
        [SerializeField] private EffectChain effectChain;
        [SerializeField] private bool isCallback = true;
        
        public Tween GetTween()
        {
            return isCallback 
                ? DOTween.Sequence().AppendCallback(() => effectChain.PlayEffect()) 
                : effectChain.PlayEffect();
        }
    }
}