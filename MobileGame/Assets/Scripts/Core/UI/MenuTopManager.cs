using System;
using System.Collections;
using System.Collections.Generic;
using Service.Currency;
using TMPro;
using UnityEngine;

namespace Service.UI
{
    public class MenuTopManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI expText;
        private ICurrencyService _currencyService;
        public void SetUp(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            currencyService.OnAddCoins += UpdateCoinsText;
            currencyService.OnAddXP += UpdateXPText;
            currencyService.OnRemoveCoins += UpdateCoinsText; 
            UpdateCoinsText(currencyService.GetCoins());
            UpdateXPText(currencyService.GetXP());
        }

        void UpdateXPText(int xp)
        {
            expText.text = xp.ToString();
        }

        private void OnDestroy()
        {
            _currencyService.OnAddCoins -= UpdateCoinsText;
            _currencyService.OnAddXP -= UpdateXPText;

            _currencyService.OnRemoveCoins -= UpdateCoinsText; 
        }

        void UpdateCoinsText(int amount)
        {
            coinsText.text = amount.ToString();
        }
    }
}
