using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Effects
{
    public class EffectButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private EffectChain effectChain;


        public void OnPointerClick(PointerEventData eventData)
        {
            effectChain.PlayEffect();
        }
    }
}