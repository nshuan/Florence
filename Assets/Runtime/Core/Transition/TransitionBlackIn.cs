using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core.Transition
{
    [Serializable]
    public class TransitionBlackIn : ITransition
    {
        [SerializeField] private Image blackCover;
        [SerializeField] private float duration = 0.3f;
        
        public void Init(Transform target)
        {
            blackCover.gameObject.SetActive(true);
        }

        public Tween DoTransition()
        {
            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    blackCover.color += new Color(0f, 0f, 0f, 1f);
                    blackCover.gameObject.SetActive(true);
                })
                .Append(blackCover.DOFade(0f, duration))
                .AppendCallback(() => blackCover.gameObject.SetActive(false));
        }
    }
}