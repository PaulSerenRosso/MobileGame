using System.Collections;
using System.Collections.Generic;
using Service;
using UnityEngine;

namespace Service.Shop 
{
public interface IShopService : IService
{

   bool GetBundleIsEnabled { get; }
   bool GetItemDaily(int index);
   void EnableBundle();

   void DisableBundle();

   void EnableItemDaily(int index);

   void DisableItemDaily(int index);
}
}
