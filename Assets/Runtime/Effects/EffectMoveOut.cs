using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectMoveOut : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform targetPos;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay).SetEase(Ease.InOutSine)
                .Append(target.DOMove(targetPos.position, duration).SetEase(Ease.InOutSine))
                .AppendCallback(() => target.gameObject.SetActive(false));
        }
    }
}