using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    [Serializable]
    public class EffectMoveIn : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform fromTargetPos;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            target.position = fromTargetPos.position;
            target.gameObject.SetActive(true);

            return DOTween.Sequence().SetDelay(delay)
                .Append(target.DOLocalMove(Vector3.zero, duration));
        }
    }
}