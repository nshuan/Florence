using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Chapters.Act1
{
    public class AnimSprites : MonoBehaviour
    {
        [SerializeField] private Image target;
        [SerializeReference, SubclassSelector] private AnimSpriteFrame[] frames;
        [SerializeField] private bool playOnEnable = false;
        [SerializeField] private bool loop = false;

        private void OnEnable()
        {
            if (playOnEnable)
            {
                transform.DOKill();
                Play();
            }
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public Tween Play()
        {
            var seq = DOTween.Sequence().SetTarget(transform);

            foreach (var frame in frames)
            {
                seq.AppendCallback(() =>
                    {
                        // if (target)
                            target.sprite = frame.sprite;
                    })
                    .AppendInterval(frame.duration);
            }

            if (loop) seq.SetLoops(-1);
            
            return seq;
        }
    }

    [Serializable]
    public class AnimSpriteFrame
    {
        public Sprite sprite;
        public float duration;
    }
}