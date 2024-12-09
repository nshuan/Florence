using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters
{
    public class Act : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private EffectChain transitionIn;
            
        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        public Tween DoShow()
        {
            gameObject.SetActive(true);
            return transitionIn.PlayEffect();
        }
    }
}