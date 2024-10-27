using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Core.Transition
{
    public class TransitionOut : MonoBehaviour
    {
        [SubclassSelector, SerializeReference] private ITransition _transition;

        private void Awake()
        {
            _transition.Init(transform);
        }

        public void Transition(Action onComplete)
        {
            _transition.DoTransition().OnComplete(() => onComplete?.Invoke());
        }
    }
}