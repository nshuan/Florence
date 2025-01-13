using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectDisappear : IEffectNode
    {
        [SerializeField] private Transform target;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            return DOTween.Sequence().SetDelay(delay)
                .AppendCallback(() => target.gameObject.SetActive(false));
        }
    }
}