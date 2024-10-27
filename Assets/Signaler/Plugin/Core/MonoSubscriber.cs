using UnityEngine;

namespace echo17.Signaler.Core
{
    public abstract class MonoSubscriber<T> : MonoBehaviour, ISubscriber where T : ISignal
    {
        protected virtual void Awake()
        {
            Signaler.Instance.Subscribe<T>(this, OnSignal);
        }

        protected virtual void OnDestroy()
        {
            Signaler.Instance.UnSubscribe<T>(this, OnSignal);
        }

        protected abstract bool OnSignal(T signal);
    }
    
    public abstract class MonoSubscriber<T1, T2> : MonoBehaviour, ISubscriber where T1 : ISignal where T2 : ISignal
    {
        protected virtual void Awake()
        {
            Signaler.Instance.Subscribe<T1>(this, OnSignal);
            Signaler.Instance.Subscribe<T2>(this, OnSignal);
        }

        protected virtual void OnDestroy()
        {
            Signaler.Instance.UnSubscribe<T1>(this, OnSignal);
            Signaler.Instance.UnSubscribe<T2>(this, OnSignal);
        }

        protected abstract bool OnSignal(T1 signal);
        protected abstract bool OnSignal(T2 signal);
    }
}