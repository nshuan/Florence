using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

namespace Runtime.Home
{
    public class SettingsButton : MonoBehaviour, IPointerClickHandler
    {
        private static readonly string PrefabPath = "Assets/Prefabs/UISettings.prefab";
        private static GameObject _cacheObject;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ShowPrefab();
        }

        private void ShowPrefab()
        {
            if (_cacheObject == null)
            {
                _cacheObject = Instantiate(Addressables.LoadAssetAsync<GameObject>(PrefabPath).WaitForCompletion());
            }
            
            _cacheObject.gameObject.SetActive(true);
        }
    }
}