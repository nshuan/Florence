using Runtime.CharMatch;

namespace Runtime.Chapters.Act2.Sheet
{
    public class SheetCharItem : ICharItem
    {
        public int Value { get; set; }
        public virtual string ToText()
        {
            return $"${Value}";
        }
    }
}