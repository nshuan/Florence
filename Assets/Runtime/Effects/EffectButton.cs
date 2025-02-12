using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Runtime.Effects
{
    public class EffectButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private EffectChain effectChain;
        [SerializeField] private bool canRestart = false;
        
        private bool clickable = true;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!clickable) return;
            
            clickable = false;
            effectChain.PlayEffect().OnComplete(() =>
            {
                clickable = canRestart;
            });
        }
    }
}