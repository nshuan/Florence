using System;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class ActionEatArm : MonoBehaviour
    {
        [SerializeField] private ActionEat actionEat;
        [SerializeField] private AnimSprites eatAnim;
        
        private void OnEnable()
        {
            actionEat.OnEat += OnEat;
        }

        private void OnDisable()
        {
            actionEat.OnEat -= OnEat;
        }

        private void OnEat()
        {
            eatAnim.Play();
        }
    }
}