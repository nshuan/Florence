using System;
using Runtime.CharMatch;

namespace Runtime.Chapters.Act2.Sheet
{
    [Serializable]
    public class CharMatchSheetStrategy : ICharMatchShuffleStrategy
    {
        public ICharItem[] GetShuffledChar(int row, int col)
        {
            var valueArray = new int[row * col];
            for (var i = 0; i < valueArray.Length; i += 2)
            {
                valueArray[i] = (i + 1) * 10;
                if (i + 1 < valueArray.Length) valueArray[i + 1] = (i + 1) * 10;
            }
            
            Array.Sort(valueArray, (value1, value2) => UnityEngine.Random.Range(-1, 2));

            var items = new ICharItem[valueArray.Length];
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new SheetCharItem() { Value = valueArray[i] };
            }

            return items;
        }
    }
}