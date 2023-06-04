using UnityEngine;

namespace Service.Items
{
    [CreateAssetMenu(menuName = "Items/ItemsServiceGlobalSettingsSO",
        fileName = "new ItemsServiceGlobalSettingsSO")]
    public class ItemsServiceGlobalSettingsSO : ScriptableObject
    {
        public ItemSO[] AllItemsSO;
        public ItemSO[] UnlockedItemsSO;
        public ItemSO[] StartItemsSO;
    }
}