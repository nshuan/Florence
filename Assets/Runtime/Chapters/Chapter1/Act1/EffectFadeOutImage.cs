using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectFadeOutImage : IEffectNode
    {
        [SerializeField] private Image image;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .Append(image.DOFade(0f, duration))
                .AppendCallback(() => image.gameObject.SetActive(false));
        }
    }
}