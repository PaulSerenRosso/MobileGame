using System;
using Attributes;

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
        OnAddXP?.Invoke(currentXp);
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount; 
        OnAddCoins?.Invoke(currentCoins);
    }

    public event Action<int> OnAddCoins;
    public event Action<int> OnRemoveCoins;
    public event Action<int> OnAddXP;
  

    public void RemoveCoins(int amount)
    {
        currentCoins -= amount;
        OnRemoveCoins?.Invoke(currentCoins);
    }
}
}
