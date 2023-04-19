using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "GlobalSettingsGameSO", fileName = "new Global SettingsGameSO")]
public class GlobalSettingsGameSO : ScriptableObject
{
    public EnvironmentSO[] AllEnvironmentsSO;
    [FormerlySerializedAs("allEnemyMacroSO")] public EnemyMacroSO[] AllEnemyMacroSO;

}