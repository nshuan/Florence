using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectFadeIn : IEffectNode
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            _canvasGroup.alpha = 0f;
                
            return DOTween.Sequence().SetDelay(delay)
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(true))
                .Append(_canvasGroup.DOFade(1f, duration));
        }
    }
}