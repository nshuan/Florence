using DG.Tweening;
using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Core
{
    public class ActCompleteButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ActManager.Instance.LoadAct(1, 2);

            // image.DOFade(0f, 0.5f).SetEase(Ease.Linear);
        }
    }
}