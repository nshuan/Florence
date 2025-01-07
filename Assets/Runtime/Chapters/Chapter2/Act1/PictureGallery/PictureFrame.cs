using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Chapter2.Act1.PictureGallery
{
    public class PictureFrame : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool isStraight = false;
        [SerializeField] private bool isClockwise = true;

        public static event Action OnFixed;
        
        public bool IsStraight => isStraight;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsStraight) return;
            isStraight = true;
            DoFix().Play();
            OnFixed?.Invoke();
        }

        private Tween DoFix()
        {
            if (isClockwise)
                return DOTween.Sequence()
                    .Append(transform.DOLocalRotate(new Vector3(0f ,0f, 6f), 0.5f).SetRelative().SetEase(Ease.OutQuad))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, -8f), 0.5f).SetRelative().SetEase(Ease.InOutQuad))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 2f), 0.5f).SetEase(Ease.InOutQuad))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.2f).SetEase(Ease.InQuad));
            
            return DOTween.Sequence()
                .Append(transform.DOLocalRotate(new Vector3(0f ,0f, -6f), 0.5f).SetRelative().SetEase(Ease.OutQuad))
                .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 8f), 0.5f).SetRelative().SetEase(Ease.InOutQuad))
                .Append(transform.DOLocalRotate(new Vector3(0f, 0f, -2f), 0.5f).SetEase(Ease.InOutQuad))
                .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.2f).SetEase(Ease.InQuad));
        }
    }
}