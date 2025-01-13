using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2.Alarm
{
    public class StopAlarmOnClick : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.Instance.StopThirdSound();
        }
    }
}