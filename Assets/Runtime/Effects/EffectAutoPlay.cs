using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectAutoPlay : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IEffectNode effect;
        [SerializeField] private int loop = 1;

        private void OnEnable()
        {
            DOTween.Sequence().SetTarget(transform)
                .Append(effect.GetTween())
                .SetLoops(loop).Play();
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}