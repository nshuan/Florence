using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act3
{
    public class EffectFlare : MonoBehaviour
    {
        [SerializeField] private Image target;
        [SerializeField] private float delay;
        
        private void OnEnable()
        {
            DoEffect().Play();
        }

        private Tween DoEffect()
        {
            var seq = DOTween.Sequence().SetTarget(transform);
            seq.SetLoops(-1);
            seq.SetDelay(delay);

            target.transform.localScale = Vector3.one * 0.5f;
            target.color -= new Color(0f, 0f, 0f, 1f);
            
            seq.AppendCallback(() => target.DOFade(1f, 0.8f).SetEase(Ease.InSine))
                .AppendCallback(() => target.transform.DOScale(1f, 0.8f).SetEase(Ease.InSine));
            
            seq.AppendInterval(1.5f);

            seq.AppendCallback(() => target.transform.DOScale(0.5f, 0.8f).SetEase(Ease.InSine).SetDelay(0.5f))
                .AppendCallback(() => target.DOFade(0f, 1f).SetEase(Ease.InSine));

            seq.AppendInterval(2.5f);
            
            return seq;
        }
    }
}