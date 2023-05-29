using UnityEngine;

namespace Service.Items
{
    public class ItemSO : ScriptableObject
    {
        public ItemTypeEnum Type;
        public Sprite SpriteUI;
        public int Price;
        public bool IsUnlockableWithStar;
        public int ExperienceStar;
        public bool IsUnlockableInDaily;
    }
}