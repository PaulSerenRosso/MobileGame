using System.Collections.Generic;
using System.Linq;
using Service.Currency;
using Service.Items;
using Service.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject deactivateBundlePanel;
        
        [Header("Item Panels")]
        [SerializeField] private Button[] _itemsButton;
        [SerializeField] private Image[] _itemsImage;
        [SerializeField] private TextMeshProUGUI[] _itemPricesText;
        [SerializeField] private TextMeshProUGUI _itemStarPriceText;
        
        [Header("Reload Panel")]
        [SerializeField] private int _reloadCost;
        [SerializeField] private TextMeshProUGUI _reloadCostText;

        private ICurrencyService _currencyService;
        private IItemsService _itemsService;
        private IShopService _shopService;
        private List<Button> _buttons;

        public void Setup(IItemsService itemsService, ICurrencyService currencyService, IShopService shopService)
        {
            _itemsService = itemsService;
            _currencyService = currencyService;
            _shopService = shopService;
            SetBundlePanel(shopService.GetBundleIsEnabled);
            _reloadCostText.text = _reloadCost.ToString();
            SetDailyItem();
        }

        public void BuyCoins(int amount)
        {
            _currencyService.AddCoins(amount);
        }

        private void SetBundlePanel(bool value)
        {
            deactivateBundlePanel.SetActive(!value);
        }

        public void BuyBundle()
        {
            deactivateBundlePanel.SetActive(true); 
            _shopService.DisableBundle();
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
                    _itemPricesText[index].text = itemSO.Price.ToString();
                    _itemStarPriceText.text = itemSO.ExperienceStar.ToString();
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO, indexButton));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                    if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == itemSO) == null)
                    {
                        _itemsButton[index].interactable = true;
                        _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                            new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        _itemsButton[index].interactable = false;
                        _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                            new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    }
                    
                }
                else
                {
                    _itemsImage[index].sprite = itemSO.SpriteUI;
                    _itemPricesText[index].text = itemSO.Price.ToString();
                    _itemsButton[index].onClick.AddListener(() => BuyItem(itemSO, indexButton));
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.SpriteUI;
                    if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == itemSO) == null)
                    {
                        _itemsButton[index].interactable = true;
                        _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                            new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        _itemsButton[index].interactable = false;
                        _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                            new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }

        private void BuyItem(ItemSO itemSO, int index)
        {
            switch (itemSO.IsUnlockableWithStar)
            {
                case true:
                    if (_currencyService.GetXP() < itemSO.ExperienceStar) return;
                    if (_currencyService.GetCoins() < itemSO.Price) return;
                    _itemsButton[index].interactable = false;
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                        new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    _itemsService.UnlockItem(itemSO);
                    _currencyService.RemoveCoins(itemSO.Price);
                    break;
                case false:
                    if (_currencyService.GetCoins() < itemSO.Price) return;
                    _itemsButton[index].interactable = false;
                    _itemsButton[index].transform.GetChild(0).GetComponent<Image>().color =
                        new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    _itemsService.UnlockItem(itemSO);
                    _currencyService.RemoveCoins(itemSO.Price);
                    break;
            }
        }

        public void BuyItem(ItemSO itemSO)
        {
            _itemsService.UnlockItem(itemSO);
        }

        public void ReloadItems()
        {
            if (_currencyService.GetCoins() < _reloadCost) return;
            _currencyService.RemoveCoins(_reloadCost);
            _shopService.RefreshDaily();
            SetDailyItem();
        }
    }
}