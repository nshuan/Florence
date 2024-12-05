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

        private void OnEnable()
        {
            DoAction();
        }

        private Tween DoAction()
        {
            transform.DOKill();
            foreach (var letter in zLetters)
            {
                letter.color = new Color(1f, 1f, 1f, 0.2f);
            }

            var seq = DOTween.Sequence().SetLoops(-1);

            for (var i = 0; i < zLetters.Length; i++)
            {
                var letter = zLetters[i];
                seq.Join(DOTween.Sequence()
                    .AppendInterval(delay * i)
                    .Append(letter.DOFade(1f, duration))
                    .AppendInterval(duration)
                    .Append(letter.DOFade(0.2f, duration))
                    .AppendInterval(delay));
            }

            return seq;
        }
    }
}