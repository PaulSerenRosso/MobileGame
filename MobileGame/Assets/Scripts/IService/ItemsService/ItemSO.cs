using UnityEngine;

namespace Service.Items
{
    public class ItemSO : ScriptableObject
    {
        public ItemTypeEnum Type;
        public string TitleItem;
        public string DescriptionItem;
        public Sprite SpriteUI;
        public int Price;
        public bool IsUnlockableWithStar;
        public int ExperienceStar;
    }
}