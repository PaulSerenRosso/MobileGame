using Attributes;
using Service.Currency;
using Service.Items;
using Service.Shop;
using UnityEngine;

namespace Service.UI
{
    public class MenuShopManager : MonoBehaviour
    {
        private IItemsService _itemsService;
        private IShopService _shopService;
        private ICurrencyService _currencyService;
        [SerializeField] private GameObject deactivateBundlePanel;
        [SerializeField] private GameObject[] deactivateItemPanel;

        private void SetBundlePanel(bool value)
        {
            deactivateBundlePanel.SetActive(!value);
        }

        private void SetDailyItem(bool value, int index)
        {
            deactivateItemPanel[index].SetActive(!value);
        }
        
        public void BuyItem(ItemSO itemSo)
        {
            _itemsService.UnlockItem(itemSo);
        }

        public void BuyCoins(int amount)
        {
            _currencyService.AddCoins(amount);
        }
        public void BuyBundle()
        {
            deactivateBundlePanel.SetActive(true); 
            _shopService.DisableBundle();
        }

        public void BuyDailyItem(int index)
        {
            deactivateItemPanel[index].SetActive(true);
            _shopService.DisableItemDaily(index);
        }
        public void SetUp(IItemsService itemsService, ICurrencyService currencyService, IShopService shopService )
        {
            _itemsService = itemsService;
            _currencyService = currencyService;
            _shopService = shopService;
            SetBundlePanel(shopService.GetBundleIsEnabled);
            for (int i = 0; i < deactivateItemPanel.Length; i++)
            {
                SetDailyItem(shopService.GetItemDaily(i), i);
            }
            
        }
    }
}