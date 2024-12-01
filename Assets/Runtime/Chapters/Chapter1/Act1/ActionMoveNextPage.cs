using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act1
{
    public class ActionMoveNextPage : ActionMove
    {
        [SerializeField] private Transform nextPage;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            DoMoveNextPage();
        }

        private Tween DoMoveNextPage()
        {
            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    nextPage.localPosition = - new Vector3(DirectionVector.x, DirectionVector.y) * distance;
                    nextPage.gameObject.SetActive(true);
                })
                .Append(DoAction())
                .Join(nextPage.DOLocalMove(Vector3.zero, duration));
        }
    }
}