using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Effects
{
    public class EffectShakeRotationOnEnable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Vector3 strength = new Vector3(0f, 0f, 10f);
        [SerializeField] private float delay = 3f;
        [SerializeField] private float startDelay;
        [SerializeField] private bool stopOnClick = false;
        private float duration = 0.06f;

        private void OnEnable()
        {
            DoShakeRotation().Play();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private Tween DoShakeRotation()
        {
            return DOTween.Sequence().SetTarget(transform).SetDelay(startDelay)
                .SetLoops(-1)
                .Append(transform.DORotate(strength, duration / 2f))
                .Append(transform.DORotate(-strength, duration).SetLoops(6, LoopType.Yoyo))
                .Append(transform.DORotate(Vector3.zero, duration / 2f))
                .AppendInterval(delay);
        }

        public void Stop()
        {
            transform.DOKill();
            this.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (stopOnClick)
                Stop();
        }
    }
}