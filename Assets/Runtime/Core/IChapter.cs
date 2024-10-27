using System;

namespace Runtime.Core
{
    public interface IChapter
    {
        IChapter NextChapter { get; set; }
        IChapterEntity ChapterEntity { get; set; }

        Type Type => GetType();
    }
    
    public interface IAct
    {
        
    }
}