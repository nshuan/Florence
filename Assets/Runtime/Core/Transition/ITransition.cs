using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Core.Transition
{
    public interface ITransition
    {
        void Init(Transform target);
        Tween DoTransition();
    }
}