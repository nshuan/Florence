using DG.Tweening;
using Runtime.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Core
{
    public class ActCompleteButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private int nextChapter;
        [SerializeField] private int nextAct;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            DOTween.KillAll();
            if (!ActManager.Instance.TryLoadAct(nextChapter, nextAct, out var act))
            {
                AudioManager.Instance.VolumeOffBgMusic();
                Loading.Instance.LoadScene("Home", 1f, loadedAction: null);

            }

            // image.DOFade(0f, 0.5f).SetEase(Ease.Linear);
        }
    }
}