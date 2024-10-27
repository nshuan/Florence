using System;
using System.Diagnostics;

namespace Core
{
    public static class EditorContext {

        [Conditional("UNITY_EDITOR")]
        public static void Call(Action action) {
            action?.Invoke();
        }
    }
}