using Core;
using Runtime.Chapters;
using UnityEngine;

namespace Runtime.Core
{
    public class ActManager : MonoSingleton<ActManager>
    {
        [SerializeField] private Camera mainCam;
        [SerializeField] private Transform actParent;
        
        private readonly IActLoader actLoader = new ScriptableObjectActLoader();
        
        public void LoadAct(int chapter, int act)
        {
            var actPref = actLoader.Load(chapter, act);

            var actInstance = Instantiate(actPref, actParent);
            actInstance.transform.position = Vector3.zero;

            actInstance.DoShow();
        }
    }
}