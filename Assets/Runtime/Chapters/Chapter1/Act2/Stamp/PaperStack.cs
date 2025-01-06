using System;
using DG.Tweening;
using Runtime.Core;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Chapters.Act2.Stamp
{
    public class PaperStack : MonoBehaviour
    {
        [SerializeField] private Transform[] papers;
        [SerializeField] private Transform stampInk;
        [SerializeField] private bool hideAllOnComplete = true;

        [Space] 
        [Header("Stampable Box")] 
        [SerializeField] private Vector2 boxCenter;
        [SerializeField] private Vector2 boxSize;

        private int currentTurn;
        private Vector2 lastStampPosition;
        private Transform stampInkInstance;

        private void Start()
        {
            currentTurn = papers.Length - 1;
            BlockUI.Instance.Block();
            InitPapers();
        }

        private void InitPapers()
        {
            DoInitPapers().Play().OnComplete(BlockUI.Instance.Unblock);
        }
        
        private Tween DoInitPapers()
        {
            var seq = DOTween.Sequence().AppendInterval(0.5f);
            
            foreach (var paper in papers)
            {
                paper.localPosition += new Vector3(0f, -650f, 0f);
                seq.AppendCallback(() => paper.DOLocalMoveY(-50f, 0.32f))
                    .AppendInterval(0.1f);
            }

            return seq;
        }
        
        public bool IsStampable(Vector2 position)
        {
            if (currentTurn < 0) return false;
            
            return position.x >= boxCenter.x - boxSize.x / 2 && position.x <= boxCenter.x + boxSize.x / 2
                && position.y >= boxCenter.y - boxSize.y / 2 && position.y <= boxCenter.y + boxSize.y / 2;
        }

        public void Stamp(Vector2 position)
        {
            lastStampPosition = position;
            stampInkInstance = Instantiate(stampInk, transform.parent);
            stampInkInstance.localPosition = position;
            stampInkInstance.SetParent(papers[^1]);
            stampInkInstance.gameObject.SetActive(true);
        }

        public Tween DoNextPaper()
        {
            if (currentTurn < 0) return DOTween.Sequence();

            var seq = DOTween.Sequence();
            var duplicateCount = Fibonacci(papers.Length - currentTurn);
            currentTurn -= 1;
            
            for (var i = 0; i < duplicateCount; i++)
            {
                var currentIndex = i;
                seq.AppendCallback(() =>
                    {
                        var paper = papers[^1];
                        var thisPaperStampInk = stampInkInstance;
                        var newIndex = 0;

                        if (hideAllOnComplete && currentIndex == duplicateCount - 1 && currentTurn < 0)
                        {
                            for (var j = 0; j < papers.Length - 1; j++)
                            {
                                papers[j].gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            newIndex = Random.Range(0, 3);
                            
                            // Shift elements to the right starting from the random index
                            for (var i = papers.Length - 1; i > newIndex; i--)
                            {
                                papers[i] = papers[i - 1];
                            }

                            // Place the last element at the random index
                            papers[newIndex] = paper;
                        }

                        paper.DOLocalMoveY(800, 0.25f).SetEase(Ease.OutQuad)
                            .OnComplete(() =>
                            {
                                paper.SetSiblingIndex(newIndex);
                                if (thisPaperStampInk != null) Destroy(thisPaperStampInk.gameObject);
                                paper.localPosition = papers[^1].localPosition;
                            });

                        if (currentIndex == duplicateCount - 1) return;
                        stampInkInstance = Instantiate(stampInk, transform.parent);
                        stampInkInstance.localPosition = lastStampPosition;
                        stampInkInstance.SetParent(papers[^1]);
                        stampInkInstance.gameObject.SetActive(true);
                    })
                    .AppendInterval(0.25f);
            }

            return seq;
        }

        // Start at 1
        private int Fibonacci(int n)
        {
            if (n <= 1) return 1;
            if (n == 2) return 1;
            
            if (n % 2 == 0) return Fibonacci(n / 2) * (2 * Fibonacci(n / 2 + 1) - Fibonacci(n / 2));
            else return (int)Math.Pow(Fibonacci(n / 2 + 1), 2) + (int)Math.Pow(Fibonacci(n / 2), 2);
        }
    }
}