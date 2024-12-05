using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    [Serializable]
    public class EffectWakeUp : IEffectNode
    {
        [SerializeField] private AnimSprites blinkAnim;
        [SerializeField] private Transform background;
        
        public Tween GetTween()
        {
            background.DOKill();
            var bgSeq = DOTween.Sequence(background);
            bgSeq.Append(background.DOScale(1.02f, 3f))
                .Append(background.DOScale(1f, 5f).SetEase(Ease.OutQuad))
                .SetLoops(-1);
            bgSeq.Play();
            
            return blinkAnim.Play();
        }
    }
}