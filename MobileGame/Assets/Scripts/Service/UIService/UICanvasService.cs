using Addressables;
using Attributes;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service
{
    public class UICanvasService : IUICanvasSwitchableService
    {
        private GameObject _mainMenu;

        [DependsOnService] private IGameService _gameService;
        
        public void LoadMainMenu()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("MainMenu", AssignMainMenu);
        }

        public void LoadPopUpCanvas() { }

        public void EnabledService() { }

        public void DisabledService() { }

        public bool GetIsActiveService { get; }

        private void AssignMainMenu(GameObject gameObject)
        {
            _mainMenu = Object.Instantiate(gameObject);
            _mainMenu.GetComponent<MenuManager>().SetupMenu(_gameService);
            Release(gameObject);
        }
    }
}