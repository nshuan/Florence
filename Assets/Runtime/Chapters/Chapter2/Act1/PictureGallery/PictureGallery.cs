using System;
using System.Linq;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Chapter2.Act1.PictureGallery
{
    public class PictureGallery : MonoBehaviour
    {
        [SerializeField] private EffectChain completeEffect;
        
        private PictureFrame[] pictures;
        private bool IsComplete => pictures.All((picture) => picture.IsStraight);
        
        private void Awake()
        {
            pictures = GetComponentsInChildren<PictureFrame>();

            PictureFrame.OnFixed += OnFrameFixed;
        }

        private void OnDestroy()
        {
            PictureFrame.OnFixed -= OnFrameFixed;
        }

        private void OnFrameFixed()
        {
            if (IsComplete) completeEffect.Play();
        }
    }
}