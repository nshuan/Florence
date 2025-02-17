using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectOnProgressComplete : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private EffectChain completeEffect;

        private IProgress progress;
        
        private void Start()
        {
            progress = target.GetComponent<IProgress>();
            progress.OnComplete += OnComplete;
        }

        private void OnDestroy()
        {
            progress.OnComplete -= OnComplete;
        }

        private void OnComplete()
        {
            progress.OnComplete -= OnComplete;
            completeEffect.PlayEffect();
        }

        private void OnValidate()
        {
            if (!target.TryGetComponent<IProgress>(out var component))
            {
                throw new Exception($"{this}: target must be {typeof(IProgress)}");
            }
        }
    }

    public interface IProgress
    {
        Action OnComplete { get; set; }
    }
}