using DG.Tweening;
using Runtime.Audio;
using Runtime.Core;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionOpenBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image openImage;
        [SerializeField] private Image openColorImage;
        [SerializeField] private Sprite openWhiteSprite;
        [SerializeField] private EffectChain completeEffect;
        [SerializeField] private AudioPlay sfx;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            BlockUI.Instance.Block();
            DoEffect().Play().OnComplete(BlockUI.Instance.Unblock);
        }

        private Tween DoEffect()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
                {
                    openImage.color -= new Color(0f, 0f, 0f, 1f);
                    openColorImage.color -= new Color(0f, 0f, 0f, 1f);
                    openImage.gameObject.SetActive(true);
                    openColorImage.gameObject.SetActive(true);
                })
                .Append(openImage.DOFade(1f, 1.5f))
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    sfx.Play(1f);
                    openImage.transform.localScale *= 1.2f;
                    openImage.sprite = openWhiteSprite;
                })
                .Append(openImage.transform.DOScale(1f, 3f))
                .Join(DOTween.Sequence()
                    .AppendInterval(1f)
                    .Append(openColorImage.DOFade(1f, 2f)));
            
            return seq.OnComplete(() =>
            {
                // completeEffect.PlayEffect().Play();
            });
        }
    }
}