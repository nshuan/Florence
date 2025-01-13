using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionEatProgress : MonoBehaviour
    {
        [SerializeField] private ActionEat actionEat;
        [SerializeField] private Image fill;
        [SerializeField] private int maxStage = 6;
        [SerializeField] private EffectChain completeEffect;

        private int currentStage = 0;

        private void OnEnable()
        {
            actionEat.OnEat += OnEat;
            actionEat.OnComplete += OnComplete;
        }

        private void OnDisable()
        {
            actionEat.OnEat -= OnEat;
            actionEat.OnComplete -= OnComplete;
        }

        private void OnEat()
        {
            currentStage += 1;

            transform.DOComplete();

            DOTween.Sequence().SetTarget(transform)
                .AppendInterval(0.3f)
                .Append(DOTween.To(() => fill.fillAmount, x =>
                {
                    fill.fillAmount = x;
                }, (float)currentStage / maxStage, 0.5f).SetTarget(transform))
                .Join(DOTween.Sequence()
                    .Append(transform.DOScale(1.08f, 0.2f))
                    .Append(transform.DOScale(1f, 0.3f)));
        }

        private void OnComplete()
        {
            completeEffect.PlayEffect();
            actionEat.OnComplete -= OnComplete;
        }
    }
}