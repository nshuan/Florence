using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class LoopNode : IEffectNode
    {
        [SerializeReference, SubclassSelector] private IEffectNode _effectToLoop;
        [SerializeField] private int loop = -1;
        [SerializeField] private LoopType loopType = LoopType.Restart;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().Append(_effectToLoop.GetTween()).SetLoops(loop, loopType);
        }
    }
}