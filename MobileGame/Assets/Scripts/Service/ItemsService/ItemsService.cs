using System.Collections.Generic;
using System.Linq;
using Addressables;
using Attributes;
using HelperPSR.Collections;
using Service.Shop;

namespace Service.Items
{
    public class ItemsService : IItemsService
    {
        [DependsOnService] private IShopService _shopService;
        
        private List<ItemSO> _unlockItems = new();
        private List<ItemSO> _lockedItems = new();
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
            foreach (var itemSO in _globalSettingsSO.AllItemsSO)
            {
                if (_globalSettingsSO.UnlockedItemsSO.Contains(itemSO)) _unlockItems.Add(itemSO);
                else _lockedItems.Add(itemSO);
            }
            if(_globalSettingsSO.StartItemsSO.Length == 0) return;
            foreach (var itemSO in _globalSettingsSO.StartItemsSO)
            {
                SetItemPlayer(itemSO);
            }
            _shopService.Setup();
        }

        public void UnlockItem(ItemSO itemSo)
        {
            _lockedItems.Remove(itemSo);
            _unlockItems.Add(itemSo);
        }

        public ItemSO[] GetAllItems()
        {
            return _globalSettingsSO.AllItemsSO;
        }

        public List<ItemSO> GetLockedItems()
        {
            return _lockedItems;
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
                if (hatItem.PrefabHat) _playerItemsLinker.SetHat(hatItem.PrefabHat);
                else _playerItemsLinker.RemoveHat();
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
                if (shirtItem.SpriteTexture) _playerItemsLinker.SetTShirt(shirtItem.SpriteTexture.texture);
                else _playerItemsLinker.RemoveTShirt();
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
                if (shortItem.SpriteTexture) _playerItemsLinker.SetShort(shortItem.SpriteTexture.texture);
                else _playerItemsLinker.RemoveShort();
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