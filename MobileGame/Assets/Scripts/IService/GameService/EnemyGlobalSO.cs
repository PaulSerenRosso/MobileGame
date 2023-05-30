using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "EnemyGlobalSO", fileName = "new EnemyGlobaleSO")]
public class EnemyGlobalSO : ScriptableObject
{
    public string Name;
    [FormerlySerializedAs("Sprite")] public Sprite IconSprite;
    public Sprite BannerSprite;
    public string enemyAdressableName;
}
