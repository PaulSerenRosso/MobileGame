using System.Collections;
using System.Collections.Generic;
using Attributes;
using Service.Currency;
using UnityEngine;

namespace Service.Currency
{
public class CurrencyService : ICurrencyService
{
    private int currentXp;
    private int currentCoins;

    [ServiceInit]
    private void Init()
    {
        
    }
    
    public int GetXP()
    {
        return currentXp;
    }

    public int GetCoins()
    {
        return currentCoins; 
    }

    public void AddXP(int amount)
    {
        currentXp += amount;
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount; 
    }

    public void RemoveCoins(int amount)
    {
        currentCoins -= amount;
    }
}
}
