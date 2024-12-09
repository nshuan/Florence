using System;
using UnityEngine;

namespace Runtime.Audio
{
    public class HomeBgMusic : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayBgMusic();
        }
    }
}