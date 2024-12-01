using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act1
{
    public class BlockClick : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Blocked!");
        }
    }
}