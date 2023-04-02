using Interfaces;
using Service;
using Service.Hype;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private InGameMenuSettingsManager _inGameMenuSettingsManager;

    [SerializeField] private InGameMenuHypeManager _inGameMenuHypeManager;

    [SerializeField] private InGameMenuHealthManager _inGameMenuHealthManager;

    public void SetupMenu(ISceneService sceneService, IHypeService hypeService, ILifeable interfaceLifeable, IDamageable interfaceDamageable)
    {
        _inGameMenuSettingsManager.Init(sceneService);
        _inGameMenuHypeManager.Init(hypeService);
        _inGameMenuHealthManager.Init(interfaceLifeable, interfaceDamageable);
    }
}