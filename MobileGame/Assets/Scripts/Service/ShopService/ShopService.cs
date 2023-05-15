using System.Collections;
using System.Collections.Generic;
using Attributes;
using Service.Shop;
using UnityEngine;

namespace Service.Shop
{
    
}
public class ShopService : IShopService
{
    private bool _bundleIsEnabled ;
    private bool[] _dailyItemAreEnabled = new bool[3];
    public bool GetBundleIsEnabled { get => _bundleIsEnabled; }

    [ServiceInit]
    private void Init()
    {
      EnableBundle();
      for (int i = 0; i < _dailyItemAreEnabled.Length; i++)
      {
          EnableItemDaily(i);
      }
    }
    
    public bool GetItemDaily(int index)
    {
        return _dailyItemAreEnabled[index];
    }

    public void EnableBundle()
    {
        _bundleIsEnabled = true;
    }

    public void DisableBundle()
    {
        _bundleIsEnabled = false; 
    }

    public void EnableItemDaily(int index)
    {
        _dailyItemAreEnabled[index] = true;
    }

    public void DisableItemDaily(int index)
    {
        _dailyItemAreEnabled[index] = false;
    }
}
