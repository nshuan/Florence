using System;
using DG.Tweening;
using Runtime.Core.Transition;
using UnityEngine;

namespace Runtime.Core
{
    public abstract class AbstractChapterEntity : MonoBehaviour, IChapterEntity
    {
        public IChapter Chapter { get; set; }

        public virtual void OnTransitionIn(Action onComplete)
        {
            var transition = GetComponent<TransitionIn>();
            if (transition == null) return;
            
            transition.Transition(onComplete);
        }

        public virtual void OnTransitionOut(Action onComplete)
        {
            var transition = GetComponent<TransitionOut>();
            if (transition == null) return;
            
            transition.Transition(onComplete);
        }
    }
}