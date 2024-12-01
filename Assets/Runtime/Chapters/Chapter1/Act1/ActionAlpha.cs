using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionAlpha : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected Graphic target;
        [SerializeField, Range(0f, 1f)] protected float targetAlpha;
        [SerializeField] private float duration = 0.5f;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            DoAction();
        }

        protected virtual Tween DoAction()
        {
            return target.DOFade(targetAlpha, duration);
        }
    }
}