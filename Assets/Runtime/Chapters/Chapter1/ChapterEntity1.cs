using echo17.Signaler.Core;
using Runtime.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Chapters
{
    public class ChapterEntity1 : AbstractChapterEntity, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Signaler.Instance.Broadcast(null, new ChapterCompleteSignal()
            {
                Chapter = this.Chapter
            });
        }
    }
}