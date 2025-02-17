using System;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class ActionScratch : MonoBehaviour
    {
        [SerializeField] private ScratcherInterpolate scratcher;
        [SerializeField] private EffectChain effect;

        private void OnEnable()
        {
            scratcher.OnComplete += OnComplete;
        }

        private void OnDisable()
        {
            scratcher.OnComplete -= OnComplete;
        }

        private void OnComplete()
        {
            effect.PlayEffect();
            scratcher.OnComplete -= OnComplete;
        }
    }
}