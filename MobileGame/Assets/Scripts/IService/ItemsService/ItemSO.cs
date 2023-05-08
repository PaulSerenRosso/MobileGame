using UnityEngine;

namespace Service.Items
{

    [CreateAssetMenu(menuName = "Items/ItemSO",
        fileName = "new ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public ItemTypeEnum Type;
        public Sprite Sprite;
        public GameObject Prefab;
    }
}