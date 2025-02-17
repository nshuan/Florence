using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectChain : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IEffectNode[] _effectNodes;

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public Tween PlayEffect()
        {
            var sequence = DOTween.Sequence().SetTarget(transform);
            foreach (var node in _effectNodes)
            {
                sequence.Append(node.GetTween());
            }

            return sequence;
        }

        public void Play()
        {
            PlayEffect().Play();
        }
    }
}