using System;
using System.Collections;
using DG.Tweening;
using Runtime.Effects;
using UnityEngine;

namespace Runtime.Init
{
    public class LoadHome : MonoBehaviour
    {
        [SerializeField] private EffectChain introEffect;
        
        private IEnumerator Start()
        {
            yield return introEffect.PlayEffect().WaitForCompletion();
            Loading.Instance.LoadScene("Home", 1f, loadedEnumerator: null);
        }
    }
}