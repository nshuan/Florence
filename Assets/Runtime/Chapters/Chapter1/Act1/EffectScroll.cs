using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    [RequireComponent(typeof(ScrollRect))]
    public class EffectScroll : MonoBehaviour
    {
        [SerializeField] private bool forceStopAtEnd = false;
        [SerializeField] private UnityEvent onStopAtEnd;
        
        private ScrollRect scrollRect;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScroll);
        }

        private void OnDisable()
        {
            scrollRect.onValueChanged.RemoveListener(OnScroll);
        }

        private void OnScroll(Vector2 normalized)
        {
            const float delta = 0.025f;

            if (!forceStopAtEnd) return;
            
            if (scrollRect.horizontal && normalized.x <= delta)
            {
                scrollRect.horizontal = false;
                DOTween.To(() => scrollRect.horizontalNormalizedPosition, x =>
                {
                    scrollRect.horizontalNormalizedPosition = x;
                }, 0, 0.2f).OnComplete(() =>
                {
                    onStopAtEnd?.Invoke();
                });
                
                return;
            }

            if (scrollRect.vertical && normalized.y <= delta)
            {
                scrollRect.vertical = false;
                DOTween.To(() => scrollRect.verticalNormalizedPosition, x =>
                {
                    scrollRect.verticalNormalizedPosition = x;
                }, 0, 0.2f).OnComplete(() =>
                {
                    onStopAtEnd?.Invoke();
                });
                
                return;
            }
        }
        
    }
}