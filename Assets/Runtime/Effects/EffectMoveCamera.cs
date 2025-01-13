using Core;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Effects
{
    public class EffectMoveCamera : IEffectNode
    {
        [SerializeField] private Transform direction;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        public Tween GetTween()
        {
            var cam = CameraUtility.Main;
            if (cam == null) cam = Camera.main;

            var mask = CameraUtility.Instance.GameViewMask;

            var screenPosition = RectTransformUtility.WorldToScreenPoint(cam, direction.position);
            var worldPosition = cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, cam.nearClipPlane));

            // Update the camera's position
            return DOTween.Sequence().SetDelay(delay).SetEase(Ease.Linear)
                .Append(DOTween.To(() => cam.transform.position, x =>
                {
                    cam.transform.position = x;
                    mask.transform.position = x;
                }, cam.transform.position + worldPosition, duration).SetEase(Ease.Linear));
        }
    }
}