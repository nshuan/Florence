using DG.Tweening;
using Runtime.Core;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Chapters.Chapter2.Act1.LetterScene
{
    public class LetterSceneLetter : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform bigLetter;
        [SerializeField] private Image bigLetterCover;
        [SerializeField] private EffectChain nextButtonEffectChain;

        private int readTime;

        public void OnPointerClick(PointerEventData eventData)
        {
            readTime += 1;
            DoShowBigLetter().Play();
        }

        private Tween DoShowBigLetter()
        {
            var seq = DOTween.Sequence().SetTarget(transform);

            seq.AppendCallback(() =>
                {
                    bigLetter.localPosition = new Vector3(0f, -2000f, 0f);
                    bigLetter.gameObject.SetActive(true);
                    bigLetterCover.color -= new Color(0f, 0f, 0f, 1f);
                    bigLetterCover.gameObject.SetActive(true);
                    BlockUI.Instance.Block();
                })
                .Append(bigLetter.DOLocalMove(Vector3.zero, 0.5f))
                .Join(bigLetterCover.DOFade(1f, 0.5f));

            if (readTime == 1)
                seq.AppendCallback(() => nextButtonEffectChain.Play());
            
            seq.AppendInterval(2f)
                .AppendCallback(BlockUI.Instance.Unblock);
            
            return seq;
        }
    }
}