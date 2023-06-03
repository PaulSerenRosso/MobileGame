using Service.Items;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "EnemyGlobalSO", fileName = "new EnemyGlobalSO")]
public class EnemyGlobalSO : ScriptableObject
{
    public string Name;
    [FormerlySerializedAs("Sprite")] public Sprite IconSprite;
    public Sprite BannerSprite;
    [FormerlySerializedAs("enemyAdressableName")] public string EnemyAddressableName;
    public ItemSO ItemSO;
}
