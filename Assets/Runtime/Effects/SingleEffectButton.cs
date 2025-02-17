using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Effects
{
    public class SingleEffectButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeReference, SubclassSelector] private IEffectNode effect;

        private bool clickable = true;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!clickable) return;
            clickable = false;
            effect.GetTween().Play().OnComplete(() => clickable = true);
        }
    }
}