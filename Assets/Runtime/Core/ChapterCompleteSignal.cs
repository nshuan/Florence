using echo17.Signaler.Core;

namespace Runtime.Core
{
    public class ChapterCompleteSignal : ISignal
    {
        public IChapter Chapter;
    }
}