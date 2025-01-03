using System;
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
            
            if (foodList.Count == 0) OnComplete?.Invoke();
        }
    }
}