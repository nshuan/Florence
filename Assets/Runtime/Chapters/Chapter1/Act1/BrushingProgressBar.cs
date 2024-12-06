using System;
using Runtime.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class BrushingProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private int totalTurn = 100;
        [SerializeField] private EffectChain completeEffect;

        private int _currentTurn = 0;
        private bool _isComplete;
        
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

            if (!_isComplete && _currentTurn >= totalTurn)
            {
                _isComplete = true;
                completeEffect.PlayEffect();
            }
        }
    }
}