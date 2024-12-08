using Core;
using Runtime.Chapters;
using UnityEngine;

namespace Runtime.Core
{
    public class ActManager : MonoSingleton<ActManager>
    {
        [SerializeField] private Camera mainCam;
        
        private readonly IActLoader actLoader = new AddressableActLoader();
        
        public void LoadAct(int chapter, int act)
        {
            var actPref = actLoader.Load(chapter, act);

            var actInstance = Instantiate(actPref, null);
            actInstance.transform.position = Vector3.zero;
            actInstance.SetCam(mainCam);

            actInstance.DoShow();
        }
    }
}