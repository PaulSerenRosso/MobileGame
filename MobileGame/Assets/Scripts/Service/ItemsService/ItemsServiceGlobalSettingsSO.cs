using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Service.Items
{
  [CreateAssetMenu(menuName = "Items/ItemsServiceGlobalSettingsSO",
    fileName = "new ItemsServiceGlobalSettingsSO")]
  public class ItemsServiceGlobalSettingsSO : ScriptableObject
  {
    public ItemSO[] AllItemsSO;
    public ItemSO[] StartItemsSO;
  }
}