using Runtime.Chapters;

namespace Runtime.Core
{
    public class ScriptableObjectActLoader : IActLoader
    {
        public Act Load(int chapter, int act)
        {
            return ActsManifest.Default.Get(chapter, act);
        }
    }
}