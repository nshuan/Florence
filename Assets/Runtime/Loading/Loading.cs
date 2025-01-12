using System;
using System.Collections;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class Loading : MonoSingleton<Loading>
    {
        [SerializeField] private CanvasGroup visual;

        protected override void Awake()
        {
            base.Awake();

            visual.alpha = 0f;
        }

        public void LoadScene(string sceneName, float hideDuration, IEnumerator loadedEnumerator = null)
        {
            DOTween.KillAll();
            DoShow().OnComplete(() =>
            {
                StartCoroutine(IELoadScene(sceneName, loadedEnumerator, hideDuration));
            });
        }

        private IEnumerator IELoadScene(string sceneName, IEnumerator loadedEnumerator, float duration)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            if (loadedEnumerator != null)
                yield return loadedEnumerator;
            yield return DoHide(duration);
        }

        public void LoadScene(string sceneName, float hideDuration, Action loadedAction = null)
        {
            DOTween.KillAll();
            DoShow().OnComplete(() =>
            {
                StartCoroutine(IELoadScene(sceneName, loadedAction, hideDuration));
            });
        }
        
        private IEnumerator IELoadScene(string sceneName, Action loadedAction, float hideDuration)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            loadedAction?.Invoke();
            yield return DoHide(hideDuration);
        }
        
        public Tween DoShow()
        {
            transform.DOKill();
            
            return DOTween.Sequence()
                .AppendCallback(() => visual.gameObject.SetActive(true))
                .Append(visual.DOFade(1f, 0.3f))
                .AppendInterval(0.2f);
        }

        public Tween DoHide(float duration)
        {
            transform.DOKill();
            
            return DOTween.Sequence()
                .Append(visual.DOFade(0f, 0.3f))
                .AppendCallback(() => visual.gameObject.SetActive(false));
        }
    }
}