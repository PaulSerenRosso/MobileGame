using System.Collections.Generic;
using Service.Items;

namespace Service.Shop
{
    public interface IShopService : IService
    {
        bool GetBundleIsEnabled { get; }
        bool GetDailyIsEnabled { get;  }
        List<ItemSO> GetItemDaily();
        void RefreshDaily();
        void EnableBundle();
        void DisableBundle();
    }
}