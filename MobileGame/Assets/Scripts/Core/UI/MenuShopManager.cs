using System.Collections.Generic;
using Service.Currency;
using Service.Items;
using Service.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject deactivateBundlePanel;

        [SerializeField] private Image _firstItemImage;
        [SerializeField] private Button[] _itemsButton;
        [SerializeField] private Image _secondItemImage;
        [SerializeField] private Image _thirdItemImage;

        private GameObject[] deactivateItemPanel;
        private IItemsService _itemsService;
        private IShopService _shopService;
        private ICurrencyService _currencyService;
        private List<Button> _buttons;

        public void Setup(IItemsService itemsService, ICurrencyService currencyService, IShopService shopService )
        {
            _itemsService = itemsService;
            _currencyService = currencyService;
            _shopService = shopService;
            SetBundlePanel(shopService.GetBundleIsEnabled);
            SetDailyItem();
        }

        private void SetBundlePanel(bool value)
        {
            deactivateBundlePanel.SetActive(!value);
        }

        private void SetDailyItem()
        {
            var shopService = _shopService.GetItemDaily();
            for (var index = 0; index < _itemsButton.Length; index++)
            {
                var itemSO = shopService[index];
                if (itemSO.IsUnlockableWithStar)
                {
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO));
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                }
                else
                {
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO));
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                }
            }
        }

        public void BuyItem(ItemSO itemSo)
        {
            if (_currencyService.GetCoins() < itemSo.Price) return; 
            _itemsService.UnlockItem(itemSo);
            _currencyService.RemoveCoins(itemSo.Price);
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

        public void ReloadItems()
        {
            _shopService.RefreshDaily();
            SetDailyItem();
        }
    }
}