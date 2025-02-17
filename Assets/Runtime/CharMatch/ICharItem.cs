namespace Runtime.CharMatch
{
    public interface ICharItem
    {
        int Value { get; set; }
        string ToText();
    }

    public class CharItem : ICharItem
    {
        public int Value { get; set; }
        public virtual string ToText()
        {
            return Value.ToString();
        }
    }
}