using DG.Tweening;
using echo17.Signaler.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.CharMatch
{
    public class UICharItem : MonoBehaviour, IUICharItem, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Text charText;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color matchColor;
        [SerializeField] private Color unMatchColor;

        private bool Clickable { get; set; } = true;
        public ICharItem Item { get; set; }
        public void Set(ICharItem item)
        {
            Item = item;
            SetupVisual();
        }

        public Tween DoSelect()
        {
            return DOTween.Sequence(transform)
                .Join(image.DOColor(selectedColor, 0.2f))
                .Join(charText.transform.DOScale(1.2f, 0.2f));
        }

        public Tween DoUnSelect()
        {
            return DOTween.Sequence(transform)
                .Join(image.DOColor(normalColor, 0.2f))
                .Join(charText.transform.DOScale(1f, 0.2f));
        }

        private void SetupVisual()
        {
            image.color = normalColor;
            charText.transform.localScale = Vector3.zero;
            charText.text = Item.ToText();
        }

        public Tween DoAppear()
        {
            return charText.transform.DOScale(1f, 0.5f);
        }

        public Tween DoMatch()
        {
            Clickable = false;
            return DOTween.Sequence(transform)
                .Append(image.DOColor(matchColor, 0.1f))
                .Join(DOTween.To(() => Item.Value, x =>
                {
                    Item.Value = x;
                    charText.text = Item.ToText();
                }, 0, 0.5f))
                .Append(image.DOColor(normalColor, 0.2f));
        }

        public Tween DoUnMatch()
        {
            return DOTween.Sequence(transform)
                .Append(image.DOColor(unMatchColor, 0.2f))
                .AppendInterval(0.1f)
                .Append(DoUnSelect());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Clickable) return;

            Signaler.Instance.Broadcast(null, new CharItemSignal() { SelectedItem = this });
        }
    }
}