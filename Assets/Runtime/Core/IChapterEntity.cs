using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Core
{
    public interface IChapterEntity
    {
        IChapter Chapter { get; set; }
        void OnTransitionIn(Action onComplete);
        void OnTransitionOut(Action onComplete);
        
        GameObject GameObject 
        {
            get
            {
                if (this is Component component) {
                    return component.gameObject;
                }

                throw new Exception(this.GetType().FullName + " is not " + typeof(Component).FullName);
            }
        }
    }
}