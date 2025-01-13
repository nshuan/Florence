using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act2.Cleaning
{
    public class CleaningBox : MonoBehaviour, IProgress
    {
        private CleaningItem[] items;
        private Vector2 boxSize;

        private int completeItemCount = 0;

        public Vector2 BoxSize => boxSize;
        
        private void Awake()
        {
            items = GetComponentsInChildren<CleaningItem>();
            foreach (var item in items)
            {
                item.Box = this;
            }
            
            boxSize = transform.GetComponent<RectTransform>().rect.size;
        }

        public void PutInBox(CleaningItem item)
        {
            completeItemCount += 1;
            DoClampItem(item);
            
            if (completeItemCount == items.Length)
                OnComplete.Invoke();
        }

        private Tween DoClampItem(CleaningItem item)
        {
            var seq = DOTween.Sequence(transform);

            seq.Append(item.transform.DOScale(0f, 0.8f).SetEase(Ease.InBack));
            seq.AppendCallback(() => item.gameObject.SetActive(false));
            
            return seq;
        }

        public Action OnComplete { get; set; }
    }
}