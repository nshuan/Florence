using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class JoinNode : IEffectNode
    {
        [SerializeField] private float delay;
        [SerializeReference, SubclassSelector] private IEffectNode[] subNodes;
        
        public Tween GetTween()
        {
            var sequence = DOTween.Sequence();

            foreach (var node in subNodes)
            {
                sequence.Join(node.GetTween());
            }

            if (delay > 0)
            {
                sequence.SetDelay(delay);
            }

            return sequence;
        }
    }
}