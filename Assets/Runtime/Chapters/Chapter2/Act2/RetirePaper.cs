using System;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2
{
    public class RetirePaper : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject close;
        [SerializeField] private GameObject open;
        [SerializeField] private EffectChain openEffect;

        private bool isOpen = false;
        
        private void Start()
        {
            open.SetActive(isOpen);
            close.SetActive(!isOpen);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isOpen)
            {
                isOpen = true;
                openEffect.PlayEffect().SetDelay(1.6f).Play();
            }
            
            open.SetActive(!open.activeSelf);
            close.SetActive(!close.activeSelf);
        }
    }
}