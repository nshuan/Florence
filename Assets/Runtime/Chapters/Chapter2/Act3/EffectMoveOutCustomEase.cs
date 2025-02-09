using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act3
{
    [Serializable]
    public class EffectMoveOutCustomEase : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform targetPos;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private Ease ease = Ease.InOutSine;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .Append(target.DOMove(targetPos.position, duration).SetEase(ease))
                .AppendCallback(() => target.gameObject.SetActive(false));
        }
    }
}