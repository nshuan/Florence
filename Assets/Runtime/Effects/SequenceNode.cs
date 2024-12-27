using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class SequenceNode : IEffectNode
    {
        [SerializeField] private float delay;
        [SerializeReference, SubclassSelector] private IEffectNode[] nodes;
        
        public Tween GetTween()
        {
            var sequence = DOTween.Sequence();

            foreach (var node in nodes)
            {
                sequence.Append(node.GetTween());
            }

            if (delay > 0)
            {
                sequence.SetDelay(delay);
            }

            return sequence;
        }
    }
}