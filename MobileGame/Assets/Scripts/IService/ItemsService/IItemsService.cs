using System.Collections;
using System.Collections.Generic;
using Service;
using UnityEngine;

namespace Service.Items
{
    public interface IItemsService : IService
    {
        void UnlockItem(ItemSO itemSo);
        ItemSO[] GetAllItems();
        void LinkItemPlayer();
        void SetItemPlayer(ItemSO itemSo);
        void SetPlayerItemLinker(PlayerItemsLinker linker);
        Dictionary<ItemTypeEnum, ItemSO> GetPlayerItems();

        void RemoveItemPlayer(ItemTypeEnum typeEnum);

    }
}