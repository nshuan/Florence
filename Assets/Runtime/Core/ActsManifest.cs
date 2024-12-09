using System;
using Runtime.Chapters;
using UnityEngine;

namespace Runtime.Core
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Acts Manifest", fileName = "ActsManifest", order = 1)]
    public class ActsManifest : ScriptableObject
    {
        [SerializeField] private ChapterData[] chapters;

        public Act Get(int chapter, int act)
        {
            if (chapter < 1 || chapter > chapters.Length) return null;
            if (act < 1 || act > chapters[chapter - 1].acts.Length) return null;

            return chapters[chapter - 1].acts[act - 1];
        }
        
        #region Singleton

        private const string Path = "ScriptableObjects/ActsManifest";

        private static ActsManifest manifest;
        public static ActsManifest Default
        {
            get
            {
                if (manifest == null)
                    manifest = Resources.Load<ActsManifest>(Path);

                return manifest;
            }
        }

        #endregion
        
        [Serializable]
        public class ChapterData
        {
            public Act[] acts;
        }
    }
    
}