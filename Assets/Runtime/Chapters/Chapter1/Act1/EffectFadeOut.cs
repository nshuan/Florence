using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectFadeOut : IEffectNode
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float duration = 0.5f;
        
        public Tween GetTween()
        {
            return DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, duration))
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(false));
        }
    }
}