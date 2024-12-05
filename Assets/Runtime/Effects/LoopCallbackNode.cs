using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class LoopCallbackNode : IEffectNode
    {
        [SerializeReference, SubclassSelector] private IEffectNode _effectToLoop;
        [SerializeField] private int loop = -1;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().AppendCallback(() =>
            {
                _effectToLoop.GetTween().SetLoops(loop);
            });
        }
    }
}