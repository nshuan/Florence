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

        public Tween Play()
        {
            var seq = DOTween.Sequence();

            foreach (var frame in frames)
            {
                seq.AppendCallback(() => target.sprite = frame.sprite)
                    .AppendInterval(frame.duration);
            }

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