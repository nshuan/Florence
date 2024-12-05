using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionLightOn : ActionAlpha
    {
        [SerializeField] private Image puller;
        [SerializeField] private CanvasGroup canvasGroup;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            DoPull().OnComplete(() => DoAction());
        }

        protected override Tween DoAction()
        {
            return DOTween.Sequence()
                .Join(canvasGroup.DOFade(0f, 0.5f))
                .AppendCallback(() =>
                {
                    target.gameObject.SetActive(false);
                });
        }

        private Tween DoPull()
        {
            return DOTween.Sequence()
                .Append(puller.transform.DOLocalMoveY(-80f, 0.3f).SetEase(Ease.OutQuad).SetRelative())
                .Append(puller.transform.DOLocalMoveY(60f, 0.3f).SetEase(Ease.InQuad).SetRelative());
        }
    }
}