using System;
using DG.Tweening;
using Runtime.Chapters.Act2.Puzzle;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act2
{
    public class DoublePuzzle : MonoBehaviour, IProgress
    {
        [SerializeField] private PuzzleBoard puzzle1;
        [SerializeField] private PuzzleBoard puzzle2;

        private int completed = 0;
        
        private void Awake()
        {
            puzzle1.OnComplete += OnOnePuzzleComplete;
            puzzle2.OnComplete += OnOnePuzzleComplete;
        }

        private void OnDestroy()
        {
            puzzle1.OnComplete -= OnOnePuzzleComplete;
            puzzle2.OnComplete -= OnOnePuzzleComplete;
        }

        private void OnOnePuzzleComplete()
        {
            completed += 1;
            if (completed == 2)
                Complete();
        }

        private void Complete()
        {
            var seq = DOTween.Sequence(transform);

            seq.Join(puzzle1.DoComplete(20f))
                .Join(puzzle2.DoComplete(20f));

            seq.Play().OnComplete(() =>
            {
                OnComplete?.Invoke();
            });
        }

        public Action OnComplete { get; set; }
    }
}