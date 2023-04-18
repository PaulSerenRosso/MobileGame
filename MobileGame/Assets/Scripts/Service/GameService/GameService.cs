using Addressables;
using Attributes;
using Service.Fight;
using Service.Inputs;
using Service.UI;
using UnityEngine;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        [DependsOnService] private IInputService _inputService;

        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IFightService _fightService;
        
        public GlobalSettingsGameSO GlobalSettingsSO
        {
            get => _globalSettingsSO;
        }

        private GlobalSettingsGameSO _globalSettingsSO;

        // todo: get so with all environment string addressable
        // todo: launch fight

        [ServiceInit]
        private void Initialize()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GlobalSettingsGameSO>("GlobalSettingsGame",
                LoadGlobalSettingsSO);
        }

        public void LoadGameScene(string environementName)
        {
            _sceneService.LoadScene((asyncOperation)=>_fightService.StartFight(environementName));
        }
        public void LoadMainMenuScene()
        {
            _sceneService.LoadScene("MenuScene",(asyncOperation)=>_canvasService.LoadMainMenu());
        }
        private void LoadGlobalSettingsSO(GlobalSettingsGameSO so)
        {
            _globalSettingsSO = so;
            LoadMainMenuScene();
        }
    }
}