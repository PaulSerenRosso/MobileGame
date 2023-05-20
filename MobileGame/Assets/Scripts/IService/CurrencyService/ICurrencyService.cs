using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service.Currency
{
public interface ICurrencyService : IService
{
    
    int GetXP();

    int GetCoins();

    void AddXP(int amount);

    void AddCoins(int amount);

    event Action<int> OnAddCoins;
    event Action<int> OnRemoveCoins;
    
    event Action<int> OnAddXP;
  

    void RemoveCoins(int amount);
}
    
}
