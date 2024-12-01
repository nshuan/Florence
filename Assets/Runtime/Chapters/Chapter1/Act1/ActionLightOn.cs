using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionLightOn : ActionAlpha
    {
        [SerializeField] private Image puller;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            DoPull().OnComplete(() => DoAction());
        }

        protected override Tween DoAction()
        {
            return DOTween.Sequence()
                .Join(target.DOFade(0f, 0.5f))
                .Join(puller.DOFade(0f, 0.5f))
                .AppendCallback(() =>
                {
                    target.gameObject.SetActive(false);
                    puller.gameObject.SetActive(false);
                });
        }

        private Tween DoPull()
        {
            return puller.transform.DOLocalMoveY(-80f, 0.5f).SetEase(Ease.OutBack).SetRelative();
        }
    }
}