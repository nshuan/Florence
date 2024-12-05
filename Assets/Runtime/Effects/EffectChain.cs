using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectChain : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IEffectNode[] _effectNodes;

        public Tween PlayEffect()
        {
            var sequence = DOTween.Sequence();
            foreach (var node in _effectNodes)
            {
                sequence.Append(node.GetTween());
            }

            return sequence;
        }
    }
}