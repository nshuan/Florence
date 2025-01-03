using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Core
{
    public class ActCompleteButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.Instance.VolumeOffBgMusic();
            Loading.Instance.LoadScene("Home", loadedAction: null);    
        }
    }
}