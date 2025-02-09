using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act3
{
    public class EffectCredit : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private int pageNum = 3;
        [SerializeField] private float pageDuration = 5f;
        [SerializeField] private EffectChain completeEffect;

        private Tween DoEffect()
        {
            var seq = DOTween.Sequence().SetTarget(transform);

            seq.Append(DOTween.To(() => scrollRect.verticalNormalizedPosition, (x) =>
            {
                scrollRect.verticalNormalizedPosition = x;
            }, 0, pageNum * pageDuration).SetEase(Ease.Linear));
            
            return seq;
        }

        public void PlayEffect()
        {
            transform.DOKill();
            DoEffect().Play()
                .OnComplete(() => completeEffect.Play());
        }
    }
}