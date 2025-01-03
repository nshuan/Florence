using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Core
{
    [DefaultExecutionOrder(-1000)]
    public class SetDefaultDOTweenEase : MonoBehaviour
    {
        [SerializeField] private Ease defaultEase = Ease.Linear;

        private void Awake()
        {
            DOTween.defaultEaseType = defaultEase;
        }
    }
}