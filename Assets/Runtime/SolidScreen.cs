using Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime
{
    public class SolidScreen : MonoSingleton<SolidScreen>
    {
        [SerializeField] private Image image;
        
        public void Show(Color color)
        {
            image.color = color;
            image.gameObject.SetActive(true);
        }

        public void Hide()
        {
            image.gameObject.SetActive(false);
        }
    }
}