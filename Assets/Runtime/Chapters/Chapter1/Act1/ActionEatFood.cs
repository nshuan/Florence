using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act1
{
    public class ActionEatFood : MonoBehaviour, IPointerClickHandler
    {
        public Action<ActionEatFood> onEatFood;
        public bool IsBlock { get; set; } = false;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsBlock) return;
            
            DoDisappear().Play().OnComplete(() => gameObject.SetActive(false));
            
            onEatFood?.Invoke(this);    
        }

        private Tween DoDisappear()
        {
            return DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
        }
    }
}