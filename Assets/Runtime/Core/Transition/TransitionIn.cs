using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Core.Transition
{
    public class TransitionIn : MonoBehaviour
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