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
        void LinkItemPlayer(ItemSO itemSo);
        void SetItemPlayer(ItemSO itemSo);
        void SetPlayerItemLinker(PlayerItemsLinker linker);
        Dictionary<ItemTypeEnum, ItemSO> GetPlayerItems();

    }
}