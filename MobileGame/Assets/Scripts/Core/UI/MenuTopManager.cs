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
        public void SetUp(ICurrencyService currencyService)
        {
            currencyService.OnAddCoins += UpdateCoinsText;
            currencyService.OnRemoveCoins += UpdateCoinsText; 
            UpdateCoinsText(currencyService.GetCoins());
        }

        void UpdateCoinsText(int amount)
        {
            coinsText.text = amount.ToString();
        }
    }
}
