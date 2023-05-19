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

        [DependsOnService] private ITournamentService _tournamentService;

        public GlobalSettingsGameSO GlobalSettingsSO
        {
            get => _globalSettingsSO;
        }

        private GlobalSettingsGameSO _globalSettingsSO;

        [ServiceInit]
        private void Initialize()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GlobalSettingsGameSO>("GlobalSettingsGame",
                LoadGlobalSettingsSO);
        }

        public void LoadGameScene(string environmentAddressableName, string enemyAddressableName, bool isDebugFight, bool isTutorialFight)
        {
            _sceneService.LoadScene();
            _fightService.StartFight(environmentAddressableName, enemyAddressableName, isDebugFight, isTutorialFight);
        }

        public void LoadMainMenuScene()
        {
            _sceneService.LoadScene("MenuScene");
            _canvasService.LoadMainMenu();
        }

        private void LoadGlobalSettingsSO(GlobalSettingsGameSO so)
        {
            _globalSettingsSO = so;
            _tournamentService.Setup(GlobalSettingsSO.AllEnvironmentsSO,
                GlobalSettingsSO.AllEnemyGlobalSO);
            LoadMainMenuScene();
            // LoadGameScene("Coliseum", "ArnoldiosTutorialPrefab", false, true);
        }
    }
}