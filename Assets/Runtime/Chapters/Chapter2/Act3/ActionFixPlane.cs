using Runtime.Effects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act3
{
    public class ActionFixPlane : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private EffectChain fixEffect;

        private bool _isFixed = false;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isFixed) return;
            _isFixed = true;
            
            fixEffect.Play();    
        }
    }
}