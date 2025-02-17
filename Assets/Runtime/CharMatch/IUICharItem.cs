using DG.Tweening;
using echo17.Signaler.Core;

namespace Runtime.CharMatch
{
    public interface IUICharItem
    {
        ICharItem Item { get; set; }
        void Set(ICharItem item);
        Tween DoSelect();
        Tween DoUnSelect();
        Tween DoMatch();
        Tween DoUnMatch();
    }

    public class CharItemSignal : ISignal
    {
        public IUICharItem SelectedItem;
    }
}