using System.Collections;
using Runtime.Audio;
using Runtime.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Home
{
    public class UIActHomeButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int chapter;
        [SerializeField] private int act;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.Instance.VolumeOffBgMusic();
            Loading.Instance.LoadScene("InGame", OnActLoaded());    
        }

        private IEnumerator OnActLoaded()
        {
            yield return new WaitUntil(() => ActManager.Instance);
            
            ActManager.Instance.LoadAct(chapter, act);

            yield return new WaitForSeconds(0.2f);
        }
    }
}