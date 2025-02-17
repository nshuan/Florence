using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionSleep : MonoBehaviour
    {
        [SerializeField] private Image[] zLetters;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private float firstDelay = 0f;
        [SerializeField] private float alphaOnHidden = 0.2f;

        private void OnEnable()
        {
            DoAction();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private Tween DoAction()
        {
            transform.DOKill();
            foreach (var letter in zLetters)
            {
                letter.color = new Color(1f, 1f, 1f, alphaOnHidden);
            }

            var seq = DOTween.Sequence().SetTarget(transform).SetLoops(-1).SetDelay(firstDelay);

            for (var i = 0; i < zLetters.Length; i++)
            {
                var letter = zLetters[i];
                seq.Join(DOTween.Sequence().SetTarget(transform)
                    .AppendInterval(delay * i)
                    .Append(letter.DOFade(1f, duration))
                    .AppendInterval(duration)
                    .Append(letter.DOFade(alphaOnHidden, duration))
                    .AppendInterval(delay));
            }

            return seq;
        }
    }
}