using UnityEngine;

namespace Runtime.Audio
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Manifest", fileName = "AudioManifest", order = 1)]
    public class AudioManifest : ScriptableObject
    {
        [field: SerializeField] public AudioClip BgMusicHome { get; private set; }
        [field: SerializeField] public AudioClip BgMusicC1A1 { get; private set; }
        
        #region Singleton

        private const string Path = "ScriptableObjects/AudioManifest";

        private static AudioManifest manifest;
        public static AudioManifest Default
        {
            get
            {
                if (manifest == null)
                    manifest = Resources.Load<AudioManifest>(Path);

                return manifest;
            }
        }

        #endregion
    }
}