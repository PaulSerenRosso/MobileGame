using Addressables;
using Attributes;
using Service.Fight;
using Service.Hype;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service.UI
{
    public class UICanvasService : IUICanvasSwitchableService
    {
        private GameObject _mainMenu;

        [DependsOnService] private IGameService _gameService;
        
        [DependsOnService] private IFightService _fightService;

        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IHypeService _hypeService;
        public void LoadMainMenu()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("MainMenu", GenerateMainMenu);
        }

        public void LoadInGameMenu()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("InGameMenu", GenerateInGameMenu);
        }

        public void LoadPopUpCanvas() { }

        public void EnabledService() { }

        public void DisabledService() { }

        public bool GetIsActiveService { get; }

        private void GenerateMainMenu(GameObject gameObject)
        {
            _mainMenu = Object.Instantiate(gameObject);
            _mainMenu.GetComponent<MenuManager>().SetupMenu(_fightService, _gameService.GlobalSettingsSO.AllEnvironmentsAdressableName[0]);
            Release(gameObject);
        }

        private void GenerateInGameMenu(GameObject gameObject)
        {
            var inGameMenu = Object.Instantiate(gameObject);
            // Release(inGameMenu);
            inGameMenu.GetComponent<InGameMenuManager>().SetupMenu(_sceneService, _hypeService);
        }
    }
}