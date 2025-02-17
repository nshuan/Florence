using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectPullTrigger : IEffectNode
    {
        [SerializeField] private Transform puller;
        [SerializeField] private float pullLength = 80f;
        [SerializeField] private float duration = 0.6f;
        
        public Tween GetTween()
        {
            return DOTween.Sequence()
                .Append(puller.transform.DOLocalMoveY(-pullLength, duration / 2).SetEase(Ease.OutQuad).SetRelative())
                .Append(puller.transform.DOLocalMoveY(pullLength, duration / 2).SetEase(Ease.InQuad).SetRelative());
        }
    }
}