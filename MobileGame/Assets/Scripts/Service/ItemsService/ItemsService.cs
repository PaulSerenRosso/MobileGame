using System.Collections;
using System.Collections.Generic;
using Addressables;
using Attributes;
using HelperPSR.Collections;
using UnityEngine;

namespace Service.Items
{
    public class ItemsService : IItemsService
    {
        private List<ItemSO> _unlockItems = new List<ItemSO>();
        private ItemsServiceGlobalSettingsSO _globalSettingsSo;
        private PlayerItemsLinker _playerItemsLinker;
        private Dictionary<ItemTypeEnum, ItemSO> playerItems = new Dictionary<ItemTypeEnum, ItemSO>();

        [ServiceInit]
        private void Init()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<ItemsServiceGlobalSettingsSO>(
                "ItemsServiceGlobalSettingsSO", LoadGlobalSettings);
        }

        private void LoadGlobalSettings(ItemsServiceGlobalSettingsSO so)
        {
            _globalSettingsSo = so;
            for (int i = 0; i < _globalSettingsSo.StartItemsSO.Length; i++)
            {
                SetItemPlayer(_globalSettingsSo.StartItemsSO[i]);
            }
        }

        public void UnlockItem(ItemSO itemSo)
        {
            _unlockItems.Add(itemSo);
        }

        public ItemSO[] GetAllItems()
        {
            return _globalSettingsSo.AllItemsSO;
        }

        public void LinkItemPlayer(ItemSO itemSo)
        {
            switch (itemSo.Type)
            {
                case ItemTypeEnum.Foot:
                {
                    break;
                }
                case ItemTypeEnum.Hand:
                {
                    _playerItemsLinker.SetHandItem(itemSo.Prefab);
                    break;
                }
                case ItemTypeEnum.Head:
                {
                    break;
                }
            }
        }

        public void SetItemPlayer(ItemSO itemSo)
        {
            CollectionHelper.AddOrSet(ref playerItems, itemSo.Type, itemSo);
        }

        public void SetPlayerItemLinker(PlayerItemsLinker linker)
        {
            _playerItemsLinker = linker;
        }

        public Dictionary<ItemTypeEnum, ItemSO> GetPlayerItems()
        {
            return playerItems;
        }
    }
}