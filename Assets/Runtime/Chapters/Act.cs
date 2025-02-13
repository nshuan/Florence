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
        [SerializeField] public bool autoPlayBgMusic = true;
            
        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        public Tween DoShow()
        {
            gameObject.SetActive(true);
            return transitionIn.PlayEffect();
        }

        public void PlayMusic()
        {
            AudioManager.Instance.SetBgMusicAndOn(bgMusic, 1f);
        }
    }
}