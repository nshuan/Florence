using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Chapters.Act2.Puzzle
{
    public class FloatOutPieceManager : MonoBehaviour
    {
        public Transform FloatOutFromTarget { get; set; }

        private readonly Dictionary<IPuzzlePiece[], bool> floatingStateMap = new Dictionary<IPuzzlePiece[], bool>();

        private float floatDelay = 1.5f;
        private float floatSpeedScale = 0.0008f;

        private void Update()
        {
            if (floatDelay > 0)
            {
                floatDelay -= Time.deltaTime;
                return;
            }
            
            foreach (var group in floatingStateMap.Keys)
            {
                if (floatingStateMap[group]) FloatOutPieceGroup(group);
            }
        }

        private void FloatOutPieceGroup(IPuzzlePiece[] group)
        {
            if (group == null || group.Length == 0) return;

            var direction = (group[0].Transform.position - FloatOutFromTarget.position).normalized; 
            foreach (var piece in group)
            {
                piece.Transform.position += direction * floatSpeedScale;
            }
        }

        public void AddGroup(IPuzzlePiece[] group)
        {
            floatingStateMap.TryAdd(group, false);
        }

        public void SetGroupState(IPuzzlePiece[] group, bool isFloat)
        {
            floatingStateMap.TryAdd(group, false);
            floatingStateMap[group] = isFloat;
        }

        public void RemoveGroup(IPuzzlePiece[] group)
        {
            if (floatingStateMap.ContainsKey(group))
                floatingStateMap.Remove(group);
        }
    }
}