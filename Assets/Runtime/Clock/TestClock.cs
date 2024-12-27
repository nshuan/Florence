using System;
using UnityEngine;

namespace Runtime.Clock
{
    public class TestClock : MonoBehaviour
    {
        [SerializeField] private ClockMinuteHand _minuteHand;
        
        private void OnEnable()
        {
            _minuteHand.OnReset += OnHourPass;
        }

        private void OnDisable()
        {
            _minuteHand.OnReset -= OnHourPass;
        }

        private void OnHourPass()
        {
            Debug.Log("One hour pass");
        }
    }
}