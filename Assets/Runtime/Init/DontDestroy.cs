using System;
using UnityEngine;

namespace Runtime.Init
{
    [DefaultExecutionOrder(-1000)]
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}