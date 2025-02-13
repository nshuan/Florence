using Core;
using DG.Tweening;
using EasyButtons;
using Runtime.Audio;
using Runtime.Chapters;
using UnityEngine;

namespace Runtime.Core
{
    public class ActManager : MonoSingleton<ActManager>
    {
        [SerializeField] private Camera mainCam;
        [SerializeField] private Transform actParent;
        
        private readonly IActLoader actLoader = new ScriptableObjectActLoader();
        private Act currentAct;
        
        public void LoadAct(int chapter, int act)
        {
            var actPref = actLoader.Load(chapter, act);

            var actInstance = Instantiate(actPref, actParent);
            actInstance.transform.position = Vector3.zero;

            var lastAct = currentAct;
            currentAct = actInstance;
            AudioManager.Instance.VolumeOffBgMusic();
            actInstance.DoShow().OnComplete(() =>
            {
                if (actInstance.autoPlayBgMusic)
                    RestartMusic();
                if (lastAct == null) return;
                Destroy(lastAct.gameObject);
            });
        }

        public void RestartMusic()
        {
            currentAct.PlayMusic();
        }

        public bool TryLoadAct(int chapter, int act, out Act actInstance)
        {
            var actPref = actLoader.Load(chapter, act);
            if (actPref == null)
            {
                actInstance = null;
                return false;
            }
            
            actInstance = Instantiate(actPref, actParent);
            actInstance.transform.position = Vector3.zero;

            var lastAct = currentAct;
            currentAct = actInstance;
            AudioManager.Instance.VolumeOffBgMusic();
            actInstance.DoShow().OnComplete(() =>
            {
                RestartMusic();
                if (lastAct == null) return;
                Destroy(lastAct.gameObject);
            });

            return true;
        }

#if UNITY_EDITOR
        [Space]
        [Header("-- Editor Only --")]
        [SerializeField] private Act actToLoad;
        [Button]
        private void LoadAct(Act prefab)
        {
            prefab ??= actToLoad;
            currentAct = Instantiate(prefab, actParent);
            currentAct.transform.position = Vector3.zero;

            currentAct.DoShow();
            
            RestartMusic();
        }
#endif
    }
}