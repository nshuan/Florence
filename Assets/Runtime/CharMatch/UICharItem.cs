using DG.Tweening;
using echo17.Signaler.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.CharMatch
{
    public class UICharItem : MonoBehaviour, IUICharItem, IPointerClickHandler
    {
        [SerializeField] private Text charText;

        private bool Clickable { get; set; } = true;
        public ICharItem Item { get; set; }
        public void Set(ICharItem item)
        {
            Item = item;
            SetupVisual();
        }

        public Tween DoSelect()
        {
            return charText.transform.DOScale(1.2f, 0.2f);
        }

        public Tween DoUnSelect()
        {
            return charText.transform.DOScale(1f, 0.2f);
        }

        private void SetupVisual()
        {
            charText.text = Item.ToText();
        }

        public Tween DoMatch()
        {
            Clickable = false;
            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    charText.text = "0";
                });
        }

        public Tween DoUnMatch()
        {
            return DoUnSelect();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Clickable) return;

            Signaler.Instance.Broadcast(null, new CharItemSignal() { SelectedItem = this });
        }
    }
}