using System;
using UnityEngine;

namespace Runtime.Init
{
    public class LoadHome : MonoBehaviour
    {
        private void Start()
        {
            Loading.Instance.LoadScene("Home", loadedEnumerator: null);
        }
    }
}