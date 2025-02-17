using System.Collections.Generic;
using System.Linq;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class PuzzleHelper
    {
        private readonly Dictionary<IPuzzlePiece, IPuzzlePiece[]> _pieceGroupMap = new();

        public int GroupCount => _pieceGroupMap.Keys.GroupBy((key) => _pieceGroupMap[key]).Count();
        public int PiecesInGroupCount => _pieceGroupMap.Keys.Count;

        public void AddGroup(IPuzzlePiece piece)
        {
            if (!_pieceGroupMap.ContainsKey(piece)) _pieceGroupMap[piece] = new IPuzzlePiece[] { piece };
        }
        
        public void ConnectGroup(IPuzzlePiece piece1, IPuzzlePiece piece2)
        {
            if (!_pieceGroupMap.ContainsKey(piece1)) _pieceGroupMap[piece1] = new IPuzzlePiece[] { piece1 };
            if (!_pieceGroupMap.ContainsKey(piece2)) _pieceGroupMap[piece2] = new IPuzzlePiece[] { piece2 };
            if (_pieceGroupMap[piece1] == _pieceGroupMap[piece2]) return;

            var newGroupSize = _pieceGroupMap[piece1].Length + _pieceGroupMap[piece2].Length;
            var newGroup = new IPuzzlePiece[newGroupSize];
            var index = 0;
            
            while (index < _pieceGroupMap[piece1].Length)
            {
                newGroup[index] = _pieceGroupMap[piece1][index];
                index += 1;
            }

            while (index < newGroupSize)
            {
                newGroup[index] = _pieceGroupMap[piece2][index - _pieceGroupMap[piece1].Length];
                index += 1;
            }
            
            foreach (var piece in newGroup)
            {
                _pieceGroupMap[piece] = newGroup;
            }
        }

        public IPuzzlePiece[] PieceGroup(IPuzzlePiece piece)
        {
            if (!_pieceGroupMap.ContainsKey(piece)) _pieceGroupMap[piece] = new IPuzzlePiece[] { piece };

            return _pieceGroupMap[piece];
        }
    }
}