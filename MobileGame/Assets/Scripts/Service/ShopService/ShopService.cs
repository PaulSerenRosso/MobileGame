using System.Collections.Generic;
using System.Linq;
using Attributes;
using HelperPSR.Collections;
using Service.Items;
using Service.Shop;

public class ShopService : IShopService
{
    [DependsOnService] private IItemsService _itemsService;

    private bool _bundleIsEnabled;
    private bool _dailyIsEnabled;
    private List<ItemSO> _dailyItems;

    public bool GetBundleIsEnabled => _bundleIsEnabled;
    public bool GetDailyIsEnabled { get; }

    [ServiceInit]
    private void Init()
    {
        EnableBundle();
        _dailyItems.Clear();
        var listUnlock = _itemsService.GetLockedItems();
        listUnlock.ShuffleList();
        if (listUnlock.FirstOrDefault(i => i.IsUnlockableWithStar == true) != null)
            _dailyItems.Add(listUnlock.FirstOrDefault(i => i.IsUnlockableWithStar));
        foreach (var itemSO in listUnlock.Where(i => i.IsUnlockableWithStar == false))
        {
            if (_dailyItems.Count > 3) break;
            _dailyItems.Add(itemSO);
        }
    }

    public List<ItemSO> GetItemDaily()
    {
        return _dailyItems;
    }

    public void RefreshDaily()
    {
        _dailyItems.Clear();
        var listUnlock = _itemsService.GetLockedItems();
        listUnlock.ShuffleList();
        if (listUnlock.FirstOrDefault(i => i.IsUnlockableWithStar == true) != null)
            _dailyItems.Add(listUnlock.FirstOrDefault(i => i.IsUnlockableWithStar));
        foreach (var itemSO in listUnlock.Where(i => i.IsUnlockableWithStar == false))
        {
            if (_dailyItems.Count > 3) break;
            _dailyItems.Add(itemSO);
        }
    }

    public void EnableBundle()
    {
        _bundleIsEnabled = true;
    }

    public void DisableBundle()
    {
        _bundleIsEnabled = false;
    }
}