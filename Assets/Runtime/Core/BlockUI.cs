using Core;
using UnityEngine;

namespace Runtime.Core
{
    public class BlockUI : MonoSingleton<BlockUI>
    {
        [SerializeField] private GameObject coverImage;

        public void Block()
        {
            coverImage.SetActive(true);
        }

        public void Unblock()
        {
            coverImage.SetActive(false);
        }
    }
}