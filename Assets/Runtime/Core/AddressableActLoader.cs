using Runtime.Chapters;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Core
{
    public interface IActLoader
    {
        Act Load(int chapter, int act);
    }
    
    public class AddressableActLoader : IActLoader
    {
        private const string ActFolderPath = "Assets/Prefabs/Acts/";
        
        public Act Load(int chapter, int act)
        {
            var actPath = ActFolderPath + $"Act_{chapter}_{act}.prefab";

            var actObj = Addressables.LoadAssetAsync<GameObject>(actPath).WaitForCompletion();

            if (actObj.TryGetComponent<Act>(out var component))
            {
                return component;
            }

            return null;
        }
    }
}