using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Home
{
    public class BackToHomeButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.Instance.VolumeOffBgMusic();
            Loading.Instance.LoadScene("Home", 1f, loadedAction: null);    
        }
    }
}