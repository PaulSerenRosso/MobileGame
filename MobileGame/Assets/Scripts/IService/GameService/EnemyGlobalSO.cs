using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemyGlobalSO", fileName = "new EnemyGlobaleSO")]
public class EnemyGlobalSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public string enemyAdressableName;
}
