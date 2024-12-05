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
            _canvasGroup.gameObject.SetActive(true);
                
            return DOTween.Sequence().SetDelay(delay)
                .Append(_canvasGroup.DOFade(1f, duration));
        }
    }
}