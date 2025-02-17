using Runtime.Clock;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class ActionTimePassed : MonoBehaviour
    {
        [SerializeField] private ClockMinuteHand minuteHand;
        [SerializeField] private int completeAtHour;
        [SerializeField] private EffectChain completeEffect;

        private int hourPassed = 0;
        
        private void OnEnable()
        {
            minuteHand.OnReset += OnOneHourPassed;
        }

        private void OnDisable()
        {
            minuteHand.OnReset -= OnOneHourPassed;
        }

        private void OnOneHourPassed()
        {
            hourPassed += 1;
            if (hourPassed >= completeAtHour)
            {
                minuteHand.OnReset -= OnOneHourPassed;   
                completeEffect.PlayEffect();
            }
        }
    }
}