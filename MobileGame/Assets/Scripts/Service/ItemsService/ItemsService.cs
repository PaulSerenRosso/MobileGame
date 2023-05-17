using System.Collections.Generic;
using Addressables;
using Attributes;
using HelperPSR.Collections;

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
            if(playerItems.Count == 0) return;
            foreach (var playerItem in playerItems)
            {
                
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