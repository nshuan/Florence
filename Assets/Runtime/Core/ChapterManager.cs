using System;
using System.Collections.Generic;
using echo17.Signaler.Core;
using Runtime.Chapters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Core
{
    public class ChapterManager : MonoSubscriber<ChapterCompleteSignal>
    {
        [SerializeField] private AbstractChapterEntity chapterEntity1;
        [SerializeField] private AbstractChapterEntity chapterEntity2;
        
        private Dictionary<IChapter, IChapterEntity> _chaptersMap;

        protected override void Awake()
        {
            base.Awake();
            
            Init();
        }

        protected void Init()
        {
            var chapter1 = new Chapter1();
            var chapter2 = new Chapter1();
            chapter1.NextChapter = chapter2;
            chapterEntity1.Chapter = chapter1;
            chapterEntity2.Chapter = chapter2;
            _chaptersMap = new Dictionary<IChapter, IChapterEntity>()
            {
                { chapter1, chapterEntity1 },
                { chapter2, chapterEntity2 }
            };
        }
        
        protected override bool OnSignal(ChapterCompleteSignal signal)
        {
            if (signal.Chapter == null) return false;
            if (!_chaptersMap.ContainsKey(signal.Chapter)) return false;
            
            var nextChapter = signal.Chapter.NextChapter;
            _chaptersMap[signal.Chapter].OnTransitionOut(() =>
            {
                var nextChapterEntity = GetChapterEntity(nextChapter);
                nextChapterEntity.GameObject.SetActive(true);
                nextChapterEntity.OnTransitionIn(null);
                _chaptersMap[signal.Chapter].GameObject.SetActive(false);
            });

            return true;
        }

        private IChapterEntity GetChapterEntity(IChapter chapter)
        {
            if (_chaptersMap.TryGetValue(chapter, out var chapterEntity)) return chapterEntity;
            
            // Instantiate new instance of chapter and add to map
            chapterEntity = LoadChapterEntity(chapter);
            _chaptersMap.Add(chapter, chapterEntity);
            return chapterEntity;
        }

        private IChapterEntity LoadChapterEntity(IChapter chapter)
        {
            return null;
        }
    }
}