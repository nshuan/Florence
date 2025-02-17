using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectFadeInImage : IEffectNode
    {
        [SerializeField] private Image image;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            image.color -= new Color(0f, 0f, 0f, 1f);
            image.gameObject.SetActive(true);
            
            return DOTween.Sequence()
                .Append(image.DOFade(1f, duration).SetEase(Ease.InSine))
                .SetDelay(delay);
        }
    }
}