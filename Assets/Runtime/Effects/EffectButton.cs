using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Effects
{
    public class EffectButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private EffectChain effectChain;

        private bool clickable = true;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!clickable) return;
            
            clickable = false;
            effectChain.PlayEffect().OnComplete(() => clickable = true);
        }
    }
}