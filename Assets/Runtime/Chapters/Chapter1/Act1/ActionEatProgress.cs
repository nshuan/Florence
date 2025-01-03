using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionEatProgress : MonoBehaviour
    {
        [SerializeField] private ActionEat actionEat;
        [SerializeField] private Image fill;
        [SerializeField] private int maxStage = 6;

        private int currentStage = 0;

        private void OnEnable()
        {
            actionEat.OnEat += OnEat;
        }

        private void OnDisable()
        {
            actionEat.OnEat -= OnEat;
        }

        private void OnEat()
        {
            currentStage += 1;

            transform.DOComplete();
            
            DOTween.To(() => fill.fillAmount, x =>
            {
                fill.fillAmount = x;
            }, (float)currentStage / maxStage, 0.5f).SetEase(Ease.InQuart).SetTarget(transform);
        }
    }
}