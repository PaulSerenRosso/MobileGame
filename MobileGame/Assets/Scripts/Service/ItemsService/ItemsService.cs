using System.Collections.Generic;
using Addressables;
using Attributes;
using HelperPSR.Collections;
using UnityEngine;

namespace Service.Items
{
    public class ItemsService : IItemsService
    {
        private List<ItemSO> _unlockItems = new();
        private ItemsServiceGlobalSettingsSO _globalSettingsSo;
        private PlayerItemsLinker _playerItemsLinker;
        private Dictionary<ItemTypeEnum, ItemSO> playerItems = new();

        [ServiceInit]
        private void Init()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<ItemsServiceGlobalSettingsSO>(
                "ItemsServiceGlobalSettingsSO", LoadGlobalSettings);
        }

        private void LoadGlobalSettings(ItemsServiceGlobalSettingsSO so)
        {
            _globalSettingsSo = so;
            if(_globalSettingsSo.StartItemsSO.Length ==0) return;
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

        public void LinkItemPlayer()
        {
            LinkHat();
            LinkShort();
            LinkTShirt();
        }

        private void LinkTShirt()
        {
            if (playerItems.ContainsKey(ItemTypeEnum.TShirt))
            {
                var tshirtItem = (GearSO)playerItems[ItemTypeEnum.TShirt];
               _playerItemsLinker.SetTShirt(tshirtItem.SpriteTexture.texture);
            }
            else
            {
                _playerItemsLinker.RemoveTShirt();
            }
        }

        private void LinkShort()
        {
            if (playerItems.ContainsKey(ItemTypeEnum.Short))
            {
                var shortItem = (GearSO)playerItems[ItemTypeEnum.Short];
               _playerItemsLinker.SetShort(shortItem.SpriteTexture.texture);
            }
            else
            {
                _playerItemsLinker.RemoveTShirt();
            }
        }

        private void LinkHat()
        {
            if (playerItems.ContainsKey(ItemTypeEnum.Head))
            {
                var hatItem = (HatSO)playerItems[ItemTypeEnum.Head];
                _playerItemsLinker.SetHat(hatItem.PrefabHat);
            }
            else
            {
                _playerItemsLinker.RemoveHat();
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

        public void RemoveItemPlayer(ItemTypeEnum typeEnum)
        {
            playerItems.Remove(typeEnum);
        }
    }
}