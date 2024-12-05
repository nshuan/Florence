using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectScaleIn : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 from = Vector3.zero;
        [SerializeField] private Vector3 to = Vector3.one;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay = 0.5f;
        
        public Tween GetTween()
        {
            target.localScale = from;
            target.gameObject.SetActive(true);

            return DOTween.Sequence().SetDelay(delay)
                .Append(target.DOScale(to, duration).SetEase(Ease.OutBack));
        }
    }
}