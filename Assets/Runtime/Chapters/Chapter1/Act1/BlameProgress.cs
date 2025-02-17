using System;
using DG.Tweening;
using Runtime.Audio;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class BlameProgress : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private EffectChain completeEffect;
        [SerializeField] private AudioClip blameAudio;

        private int currentIndex = 0;
        private bool clickable = true;
        private float delay = 1.2f;
        private bool isComplete = false;
        
        private void Awake()
        {
            image.sprite = sprites[0];
        }

        private void Start()
        {
            DOVirtual.DelayedCall(0.8f, () => AudioManager.Instance.PlayThirdSound(blameAudio, loop: true));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isComplete) return;
            if (!clickable) return;

            clickable = false;
            DOVirtual.DelayedCall(delay, () => clickable = true);
                
            currentIndex += 1;

            if (currentIndex >= sprites.Length)
            {
                isComplete = true;
                AudioManager.Instance.StopThirdSound();
                completeEffect.PlayEffect();
                return;
            }

            image.sprite = sprites[currentIndex];
        }
    }
}