using Core;
using DG.Tweening;
using EasyButtons;
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
            actInstance.DoShow().OnComplete(() =>
            {
                if (lastAct == null) return;
                Destroy(lastAct.gameObject);
            });
        }

#if UNITY_EDITOR
        [Space]
        [Header("-- Editor Only --")]
        [SerializeField] private Act actToLoad;
        [Button]
        private void LoadAct(Act prefab)
        {
            prefab ??= actToLoad;
            var actInstance = Instantiate(prefab, actParent);
            actInstance.transform.position = Vector3.zero;

            actInstance.DoShow();
        }
#endif
    }
}