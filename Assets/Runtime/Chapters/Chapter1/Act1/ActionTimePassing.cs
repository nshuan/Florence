using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Clock;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class ActionTimePassing : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private List<ActionTimePassingStage> stageData;
        [SerializeField] private ClockMinuteHand minuteHand;
        [SerializeField] private float hideDuration = 2f;
        [SerializeField] private float hideDelay = 1f;

        private int hourPassed = 0;
        private int currentStageIndex = 0;

        private void OnValidate()
        {
            // Remember to put gameObjects in scene in correct order
            stageData.Sort(((stage1, stage2) => stage1.hourToHide.CompareTo(stage2.hourToHide)));
        }

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
            Check(hourPassed);
        }

        private void Check(int hour)
        {
            if (currentStageIndex >= stageData.Count) return;
            if (hour < stageData[currentStageIndex].hourToHide) return;
            if (stageData[currentStageIndex].graphic == null) return;

            var nextStage = stageData[currentStageIndex + 1];
            nextStage.graphic.DOFade(1f, hideDuration).SetDelay(hideDelay);

            currentStageIndex += 1;
        }
        
        [Serializable]
        public class ActionTimePassingStage
        {
            public CanvasGroup graphic;
            public int hourToHide;
        }
    }
}