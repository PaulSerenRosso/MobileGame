using System.Collections;
using System.Collections.Generic;

namespace Service.Items
{
    public interface IItemsService : IService
    {
        void UnlockItem(ItemSO itemSo);
        ItemSO[] GetAllItems();
        List<ItemSO> GetLockedItems();
        List<ItemSO> GetUnlockedItems();
        void LinkItemPlayer();
        Dictionary<ItemTypeEnum, ItemSO> GetPlayerItems();
        void SetPlayerItemLinker(PlayerItemsLinker linker);
        void SetItemPlayer(ItemSO itemSo);
        void RemoveItemPlayer(ItemTypeEnum typeEnum);

    }
}