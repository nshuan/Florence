using Runtime.Core;

namespace Runtime.Chapters
{
    public class Chapter1 : IChapter
    {
        public IChapter NextChapter { get; set; }
        public IChapterEntity ChapterEntity { get; set; }
    }
}