using System.Collections.Generic;
using Addressables;
using Attributes;
using HelperPSR.Collections;

namespace Service.Items
{
    public class ItemsService : IItemsService
    {
        private List<ItemSO> _unlockItems = new();
        private ItemsServiceGlobalSettingsSO _globalSettingsSO;
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
            _globalSettingsSO = so;
            foreach (var itemSO in _globalSettingsSO.UnlockedItemsSO)
            {
                _unlockItems.Add(itemSO);
            }
            if(_globalSettingsSO.StartItemsSO.Length == 0) return;
            foreach (var itemSO in _globalSettingsSO.StartItemsSO)
            {
                SetItemPlayer(itemSO);
            }
        }

        public void UnlockItem(ItemSO itemSo)
        {
            _unlockItems.Add(itemSo);
        }

        public ItemSO[] GetAllItems()
        {
            return _globalSettingsSO.AllItemsSO;
        }

        public List<ItemSO> GetUnlockedItems()
        {
            return _unlockItems;
        }

        public void LinkItemPlayer()
        {
            LinkHead();
            LinkShort();
            LinkShirt();
        }

        private void LinkHead()
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

        private void LinkShirt()
        {
            if (playerItems.ContainsKey(ItemTypeEnum.Shirt))
            {
                var shirtItem = (GearSO)playerItems[ItemTypeEnum.Shirt];
               _playerItemsLinker.SetTShirt(shirtItem.SpriteTexture.texture);
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
                _playerItemsLinker.RemoveShort();
            }
        }

        public Dictionary<ItemTypeEnum, ItemSO> GetPlayerItems()
        {
            return playerItems;
        }

        public void SetPlayerItemLinker(PlayerItemsLinker linker)
        {
            _playerItemsLinker = linker;
        }

        public void SetItemPlayer(ItemSO itemSo)
        {
            CollectionHelper.AddOrSet(ref playerItems, itemSo.Type, itemSo);
        }

        public void RemoveItemPlayer(ItemTypeEnum typeEnum)
        {
            playerItems.Remove(typeEnum);
        }
    }
}