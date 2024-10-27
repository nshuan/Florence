using System;
using UnityEngine;

namespace Core
{
    public class CameraUtility : MonoSingleton<CameraUtility>
    {
        private static Camera Main { get; set; }

        private void Start()
        {
            Main = Camera.main;
        }

        public static Vector3 ScreenToWorldPoint(Vector3 position, float zPos = 0f)
        {
            var convertedPos = Main.ScreenToWorldPoint(position);
            convertedPos.z = zPos;
            return Main is not null ? convertedPos : position;
        }
    }
}