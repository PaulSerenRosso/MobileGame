using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private Button[] _itemsButton;
        [SerializeField] private Image[] _itemsImage;

        private GameObject[] deactivateItemPanel;
        private IItemsService _itemsService;
        private IShopService _shopService;
        private ICurrencyService _currencyService;
        private List<Button> _buttons;

        public void Setup(IItemsService itemsService, ICurrencyService currencyService, IShopService shopService)
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
                var indexButton = index;
                if (itemSO.IsUnlockableWithStar)
                {
                    _itemsImage[index].sprite = itemSO.SpriteUI;
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO, indexButton));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                    if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == itemSO) != null) 
                        _itemsButton[index].interactable = false;
                }
                else
                {
                    _itemsImage[index].sprite = itemSO.SpriteUI;
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO, indexButton));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                    if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == itemSO) != null) 
                        _itemsButton[index].interactable = false;
                }
            }
        }

        private void BuyItem(ItemSO itemSo, int index)
        {
            switch (itemSo.IsUnlockableWithStar)
            {
                case true:
                    if (_currencyService.GetXP() < itemSo.ExperienceStar) return;
                    if (_currencyService.GetCoins() < itemSo.Price) return;
                    _itemsButton[index].interactable = false;
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                        new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    _itemsService.UnlockItem(itemSo);
                    _currencyService.RemoveCoins(itemSo.Price);
                    break;
                case false:
                    if (_currencyService.GetCoins() < itemSo.Price) return;
                    _itemsButton[index].interactable = false;
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                        new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    _itemsService.UnlockItem(itemSo);
                    _currencyService.RemoveCoins(itemSo.Price);
                    break;
            }
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