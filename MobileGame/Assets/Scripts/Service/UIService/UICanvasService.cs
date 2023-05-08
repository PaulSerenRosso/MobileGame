using System;
using Addressables;
using Attributes;
using Service.Currency;
using Service.Fight;
using Service.Hype;
using Service.Items;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;
using Object = UnityEngine.Object;

namespace Service.UI
{
    public class UICanvasService : IUICanvasSwitchableService
    {
        [DependsOnService] private IGameService _gameService;
        [DependsOnService] private ITournamentService _tournamentService;
        [DependsOnService] private IFightService _fightService;
        [DependsOnService] private IHypeService _hypeService;
        [DependsOnService] private IItemsService _itemsService;
        [DependsOnService] private ICurrencyService _currencyService;
        private GameObject _mainMenu;

        public void LoadMainMenu()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("MainMenu", GenerateMainMenu);
        }

        public void LoadInGameMenu()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("InGameMenu", GenerateInGameMenu);
        }

        public void LoadPopUpCanvas()
        {
        }

        public event Action InitCanvasEvent;

        public void EnabledService()
        {
        }

        public void DisabledService()
        {
        }

        public bool GetIsActiveService { get; }

        private void GenerateMainMenu(GameObject gameObject)
        {
            _mainMenu = Object.Instantiate(gameObject);
            _mainMenu.GetComponent<MenuManager>().SetupMenu(_gameService, _tournamentService, _fightService,
                _currencyService, _itemsService);
            Release(gameObject);
        }

        private void GenerateInGameMenu(GameObject gameObject)
        {
            var inGameMenu = Object.Instantiate(gameObject);
            inGameMenu.GetComponent<InGameMenuManager>()
                .SetupMenu(_fightService, _hypeService, _tournamentService, _currencyService);
            InitCanvasEvent?.Invoke();
            Release(gameObject);
        }
    }
}