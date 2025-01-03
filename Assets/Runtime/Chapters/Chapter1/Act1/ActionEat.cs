using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class ActionEat : MonoBehaviour
    {
        private List<ActionEatFood> foodList;
        public Action OnComplete;
        public Action OnEat;

        private const float cooldown = 0.8f;

        private void Awake()
        {
            foodList = GetComponentsInChildren<ActionEatFood>(includeInactive:false).ToList();
            foreach (var food in foodList)
            {
                food.onEatFood += OnEatFood;
            }
        }

        private void OnEatFood(ActionEatFood foodItem)
        {
            if (!foodList.Contains(foodItem)) return;

            OnEat?.Invoke();
            foodItem.onEatFood = null;
            foodList.Remove(foodItem);
            foreach (var food in foodList)
            {
                food.IsBlock = true;
            }

            StartCoroutine(IEBlockInput(cooldown));
            
            if (foodList.Count == 0) OnComplete?.Invoke();
        }

        private IEnumerator IEBlockInput(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (foodList is null) yield break;

            foreach (var food in foodList)
            {
                food.IsBlock = false;
            }
        }
    }
}