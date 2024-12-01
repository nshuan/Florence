using System;
using UnityEngine;

namespace Runtime.Chapters.Act1
{
    public class BrushingArm : MonoBehaviour
    {
        [SerializeField] private GameObject[] frames;
        [SerializeField] private int fps;

        private int _currentFrame;
        
        private void Awake()
        {
            frames[0].SetActive(true);
            _currentFrame = 0;
            _cooldown = MaxCd;
            for (var i = 1; i < frames.Length; i++)
            {
                frames[i].SetActive(false);
            }

            ActionBrush.OnMove += OnBrush;
        }

        private float MaxCd => 1f / fps;
        private float _cooldown;
        private void Update()
        {
            if (_cooldown > 0) _cooldown -= Time.deltaTime;
        }

        private void OnDestroy()
        {
            ActionBrush.OnMove -= OnBrush;
        }

        private void OnBrush()
        {
            if (_cooldown <= 0)
            {
                UpdateFrame();
                _cooldown = MaxCd;
            }
        }

        private void UpdateFrame()
        {
            var nextFrame = _currentFrame + 1;
            if (nextFrame >= frames.Length) nextFrame = 0;
            
            frames[_currentFrame].SetActive(false);
            frames[nextFrame].SetActive(true);
            _currentFrame = nextFrame;
        }
    }
}