using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using echo17.Signaler.Core;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.CharMatch
{
    public class CharMatchController : MonoSubscriber<CharItemSignal>, IProgress
    {
        [SubclassSelector, SerializeReference] private ICharMatchShuffleStrategy _shuffleStrategy;
        [SerializeField] private UICharItem charItemPref;
        [SerializeField] private GridLayoutGroup boardGrid;
        [SerializeField] private Vector2Int boardSize;
        [SerializeField] private float appearNumberDelay = 0.5f;
        [SerializeField] private AnimationCurve appearNumberCurve;

        private Dictionary<int, bool> _boardValueMatchMap;
        private IUICharItem currentSelectItem;
        private bool IsBoardComplete => _boardValueMatchMap.All((pair => pair.Value == true));

        protected override void Awake()
        {
            base.Awake();
            
            Initialize();
        }

        private void Initialize()
        {
            boardGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            boardGrid.constraintCount = boardSize.x;
            
            var flattenChar = _shuffleStrategy.GetShuffledChar(boardSize.y, boardSize.x);
            _boardValueMatchMap = new Dictionary<int, bool>();

            var appearSeq = DOTween.Sequence();
            
            foreach (var charItem in flattenChar)
            {
                _boardValueMatchMap.TryAdd(charItem.Value, false);
                
                var item = Instantiate(charItemPref, transform);
                item.Set(charItem);
                item.gameObject.SetActive(true);

                appearSeq.AppendCallback(() => item.DoAppear().SetEase(appearNumberCurve))
                    .AppendInterval(0.1f);
            }

            appearSeq.SetDelay(appearNumberDelay).Play();
        }
        
        protected override bool OnSignal(CharItemSignal signal)
        {
            if (currentSelectItem == null)
            {
                if (boardSize.x * boardSize.y % 2 == 1 && _boardValueMatchMap.Count((pair) => !pair.Value) == 1)
                {
                    _boardValueMatchMap[signal.SelectedItem.Item.Value] = true;
                    MatchSuccess(signal.SelectedItem);
#if UNITY_EDITOR
                    if (IsBoardComplete) Debug.Log("Board is completed!");
#endif
                    OnComplete?.Invoke();               
                    return true;
                }
                
                currentSelectItem = signal.SelectedItem;
                SelectItem(currentSelectItem);
                return true;
            }

            if (currentSelectItem.Item.Value != signal.SelectedItem.Item.Value)
            {
                MatchFail(currentSelectItem, signal.SelectedItem);
            }
            else
            {
                if (ReferenceEquals(currentSelectItem, signal.SelectedItem))
                    UnSelectItem(currentSelectItem);
                else
                {
                    _boardValueMatchMap[currentSelectItem.Item.Value] = true;
                    MatchSuccess(currentSelectItem, signal.SelectedItem);
                    if (IsBoardComplete) Debug.Log("Board is completed!");
                }
            }

            currentSelectItem = null;
            return true;
        }

        private void MatchSuccess(IUICharItem item1, IUICharItem item2 = null)
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
                {
                    // TODO Block UI
                })
                .Append(item1.DoMatch());
                
            if (item2 != null)
                seq.Join(item2.DoMatch());
                
            seq.OnComplete(() =>
            {
                // TODO Unblock UI
            })
            .Play();
        }

        private void MatchFail(IUICharItem item1, IUICharItem item2)
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
                {
                    // TODO Block UI
                })
                .Append(item1.DoUnMatch())
                .Join(item2.DoUnMatch())
                .OnComplete(() =>
                {
                    // TODO Unblock UI
                })
                .Play();
        }

        private void SelectItem(IUICharItem item)
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
                {
                    // TODO Block UI
                })
                .Append(item.DoSelect())
                .OnComplete(() =>
                {
                    // TODO Unblock UI
                })
                .Play();
        }
        
        private void UnSelectItem(IUICharItem item)
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
                {
                    // TODO Block UI
                })
                .Append(item.DoUnSelect())
                .OnComplete(() =>
                {
                    // TODO Unblock UI
                })
                .Play();
        }

        public Action OnComplete { get; set; }
    }
}