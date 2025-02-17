using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Chapter2.Act1.LetterScene
{
    public class LetterSceneBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject closeImage;
        [SerializeField] private GameObject openImage;
        
        private bool isOpen = false;
        public bool IsOpen => isOpen;

        private void OnEnable()
        {
            closeImage.SetActive(!isOpen);
            openImage.SetActive(isOpen);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DoShrug().Play();

            if (!isOpen) Open();
        }

        private void Open()
        {
            isOpen = true;
            closeImage.SetActive(!isOpen);
            openImage.SetActive(isOpen);
        }
        
        private Tween DoShrug()
        {
            return transform.DOPunchScale(0.1f * Vector3.one, 0.5f);
        }
        
    }
}