using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters.Act2.Cleaning
{
    public class SetParentAsLastSiblingOnMove : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.parent.SetAsLastSibling();
        }
    }
}