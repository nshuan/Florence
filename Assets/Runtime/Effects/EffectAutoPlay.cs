using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectAutoPlay : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IEffectNode effect;
        [SerializeField] private int loop = 1;
        [SerializeField] private LoopType loopType;

        private void OnEnable()
        {
            DOTween.Sequence().SetTarget(transform)
                .Append(effect.GetTween())
                .SetLoops(loop, loopType).Play();
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}