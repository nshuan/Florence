using System;
using DG.Tweening;
using Runtime.Audio;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters
{
    public class Act : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private EffectChain transitionIn;
        [SerializeField] private AudioClip bgMusic;
            
        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            AudioManager.Instance.SetBgMusicAndOn(bgMusic);
        }

        public Tween DoShow()
        {
            gameObject.SetActive(true);
            return transitionIn.PlayEffect();
        }
    }
}