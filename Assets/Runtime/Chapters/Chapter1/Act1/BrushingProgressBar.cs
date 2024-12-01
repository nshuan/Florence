using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class BrushingProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private int totalTurn = 100;

        private int _currentTurn = 0;

        private void Awake()
        {
            ActionBrush.OnMove += OnBrush;
        }

        private void OnDestroy()
        {
            ActionBrush.OnMove -= OnBrush;
        }

        private void OnBrush()
        {
            _currentTurn += 1;
            fill.fillAmount = (float)_currentTurn / totalTurn;
            
            // if (_currentTurn >= totalTurn)
        }
    }
}