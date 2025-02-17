using System;
using UnityEngine;

namespace Runtime.Clock
{
    public class ClockHourHand : MonoBehaviour
    {
        [SerializeField] private ClockMinuteHand minuteHand;

        private float _currentAngle;
        
        private void Awake()
        {
            minuteHand.OnMove += OnMinuteHandMove;
        }

        private void OnDestroy()
        {
            minuteHand.OnMove -= OnMinuteHandMove;
        }

        private void OnMinuteHandMove(float deltaAngle)
        {
            // Calculate the hour hand's angle
            var deltaHourAngle = deltaAngle / 12f; // 1 hour is 30°, so minute hand moves 1/12 of that

            _currentAngle += deltaHourAngle;
            
            // Set the hour hand's rotation
            transform.rotation = Quaternion.Euler(0, 0, _currentAngle); // Offset for vertical alignment
        }
    }
}