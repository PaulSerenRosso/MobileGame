using Service;
using Service.Fight;
using Service.Hype;
using Service.UI;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private InGameMenuSettingsManager _inGameMenuSettingsManager;
    [SerializeField] private InGameMenuHypeManager _inGameMenuHypeManager;
    [SerializeField] private InGameMenuRoundManager _inGameMenuRoundManager;
    [SerializeField] private InGameMenuEndFightManager _inGameMenuEndFightManager;
    public void SetupMenu(IFightService fightService,ISceneService sceneService, IHypeService hypeService)
    {
        _inGameMenuSettingsManager.Init(sceneService);
        _inGameMenuHypeManager.Init(hypeService);
        _inGameMenuRoundManager.Init(fightService);
        _inGameMenuEndFightManager.Init(fightService);
    }
}