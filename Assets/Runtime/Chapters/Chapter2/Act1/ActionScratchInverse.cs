using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Chapter2
{
    public class ActionScratchInverse : MonoBehaviour
    {
        [SerializeField] private ScratcherInterpolateInverse scratcher;
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