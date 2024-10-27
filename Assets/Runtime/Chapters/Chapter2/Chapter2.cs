using Runtime.Core;

namespace Runtime.Chapters
{
    public class Chapter2 : IChapter
    {
        public IChapter NextChapter { get; set; }
        public IChapterEntity ChapterEntity { get; set; }
    }
}