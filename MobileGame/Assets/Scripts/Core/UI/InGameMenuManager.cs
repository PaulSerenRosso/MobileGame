using DG.Tweening;
using Service;
using Service.Hype;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    
}
public class InGameMenuManager : MonoBehaviour
{


    [SerializeField] private InGameMenuSettingsManager _inGameMenuSettingsManager;

    [SerializeField] private InGameMenuHypeManager _inGameMenuHypeManager;
 
    
    public void SetupMenu(ISceneService sceneService, IHypeService hypeService)
    {
        _inGameMenuSettingsManager.Init(sceneService);
        _inGameMenuHypeManager.Init(hypeService);
        
  
    }
    
    
  
}