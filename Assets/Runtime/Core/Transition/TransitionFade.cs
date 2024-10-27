using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core.Transition
{
    [Serializable]
    public class TransitionFade : ITransition
    {
        [Range(0f, 1f), SerializeField] private float targetAlpha;
        [SerializeField] private float duration = 0.3f;
        
        private Graphic[] _graphics;
        
        public void Init(Transform target)
        {
            _graphics = target.GetComponentsInChildren<Graphic>();
        }

        public Tween DoTransition()
        {
            var seq = DOTween.Sequence();
            
            foreach (var graphic in _graphics)
            {
                seq.Join(graphic.DOFade(targetAlpha, duration));
            }

            return seq;
        }
    }
}