using DG.Tweening;
using Service.Currency;
using Service.Fight;
using Service.Items;
using Service.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI")] 
        [SerializeField] private Button _playMatchButton;
        [SerializeField] private Button _playTournamentButton;
        [SerializeField] private GridLayoutGroup _enemySelectionGrid;
        [SerializeField] private GridLayoutGroup _environnementSelectionGrid;
        [SerializeField] private Button _environmentButton;
        [SerializeField] private Button _enemyButton;

        [SerializeField] private MenuTopManager _menuTopManager;
        [SerializeField] private Canvas _topCanvas;
        [SerializeField] private Canvas _botCanvas;
        [SerializeField] private Canvas _backgroundCanvas;

        [SerializeField] private Button _homeButton;
        [SerializeField] private MenuTournamentManager _menuTournamentManager;
        [SerializeField] private GameObject _tournamentCanvas;
        [SerializeField] private GameObject _debugFightCanvas;

        [SerializeField] private MenuShopManager _menuShopManager;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Canvas _shopCanvas;

        [SerializeField] private MenuInventoryManager _menuInventoryManager;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Canvas _inventoryCanvas;

        [SerializeField] private GameObject[] _tournaments;
        [SerializeField] private Button _leftArrowTournament;
        [SerializeField] private Button _rightArrowTournament;

        [SerializeField] private Canvas _unlockTournament;

        private int _actualTournament;
        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private string _nextEnvironmentAddressableName;
        private string _nextEnemyAddressableName;

        public void SetupMenu(IGameService gameService, ITournamentService tournamentService,
            ICurrencyService currencyService, IItemsService itemsService, IShopService shopService, PlayerItemsLinker playerItemsLinker)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
            _menuTopManager.SetUp(currencyService);
            _menuInventoryManager.Setup(itemsService, playerItemsLinker);
            _menuShopManager.SetUp(itemsService, currencyService, shopService);
            _tournaments[_actualTournament].SetActive(true);
            _leftArrowTournament.interactable = false;
            _homeButton.interactable = false;
            if (_tournamentService.GetSet())
            {
                _menuTournamentManager.SetupMenu(_gameService, _tournamentService, this);
                OpenTournamentUI();
            }
            else
            {
                _tournamentService.SetTournament();
                _menuTournamentManager.SetupMenu(_gameService, _tournamentService, this);
            }

            foreach (var enemyGlobalSo in gameService.GlobalSettingsSO.AllEnemyGlobalSO)
            {
                var button = Instantiate(_enemyButton, _enemySelectionGrid.transform);
                button.onClick.AddListener(() => SetEnemyAddressableName(enemyGlobalSo.enemyAdressableName));
                button.image.sprite = enemyGlobalSo.Sprite;
                button.GetComponentInChildren<TextMeshProUGUI>().text = enemyGlobalSo.Name;
            }

            foreach (var environmentSo in gameService.GlobalSettingsSO.AllEnvironmentsSO)
            {
                var button = Instantiate(_environmentButton, _environnementSelectionGrid.transform);
                button.onClick.AddListener(
                    () => SetEnvironmentAddressableName(environmentSo.EnvironmentAddressableName));
                button.image.sprite = environmentSo.EnvironmentSprite;
                button.GetComponentInChildren<TextMeshProUGUI>().text = environmentSo.Name;
            }
        }

        public void SetEnvironmentAddressableName(string value) => _nextEnvironmentAddressableName = value;

        public void SetEnemyAddressableName(string value) => _nextEnemyAddressableName = value;

        public void ActivateHome()
        {
            _tournamentCanvas.SetActive(true);
            _backgroundCanvas.gameObject.SetActive(true);
            _debugFightCanvas.SetActive(false);
            _shopCanvas.gameObject.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(false);
            _topCanvas.gameObject.SetActive(true);
            _botCanvas.gameObject.SetActive(true);
            _playTournamentButton.interactable = true;
            _homeButton.interactable = false;
            _shopButton.interactable = true;
            _inventoryButton.interactable = true;
        }

        public void ActivateDebug()
        {
            _debugFightCanvas.SetActive(true);
            _tournamentCanvas.SetActive(false);
            _homeButton.interactable = true;
            _shopButton.interactable = true;
            _inventoryButton.interactable = true;
        }

        public void StartFight()
        {
            _playMatchButton.interactable = false;
            if (_nextEnemyAddressableName == null)
                _nextEnemyAddressableName = _gameService.GlobalSettingsSO.AllEnemyGlobalSO[0].enemyAdressableName;
            if (_nextEnvironmentAddressableName == null)
                _nextEnvironmentAddressableName =
                    _gameService.GlobalSettingsSO.AllEnvironmentsSO[0].EnvironmentAddressableName;
            _gameService.LoadGameScene(_nextEnvironmentAddressableName, _nextEnemyAddressableName, true, false);
        }

        public void StartFightTutorial()
        {
            _playMatchButton.interactable = false;
            _gameService.LoadGameScene("Coliseum", "ArnoldiosTutorialPrefab", false, true);
        }

        public void StartTournament()
        {
            if (_actualTournament == 0)
            {
                StartFightTutorial();
                return;
            }

            if (_actualTournament != 1)
            {
                _unlockTournament.gameObject.SetActive(true);
                return;
            }

            OpenTournamentUI();
        }

        private void OpenTournamentUI()
        {
            _playTournamentButton.interactable = false;
            _tournamentCanvas.SetActive(false);
            _topCanvas.gameObject.SetActive(false);
            _botCanvas.gameObject.SetActive(false);
            _menuTournamentManager.UpdateUITournament();
        }

        public void LeftTournament()
        {
            _actualTournament -= 1;
            if (_actualTournament < 0)
            {
                _actualTournament = 0;
                _tournaments[_actualTournament].SetActive(true);
                _leftArrowTournament.interactable = _actualTournament != 0;
                _rightArrowTournament.interactable = true;
            }
            else
            {
                _leftArrowTournament.interactable = _actualTournament != 0;
                _rightArrowTournament.interactable = true;
                for (int i = _actualTournament - 1; i >= 0; i--)
                {
                    _tournaments[i].GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 2f);
                }

                _tournaments[_actualTournament].SetActive(true);
                _tournaments[_actualTournament].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 2f);
                for (int i = _actualTournament + 1; i < _tournaments.Length; i++)
                {
                    _tournaments[i].GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 2f);
                }
            }
        }

        public void RightTournament()
        {
            _actualTournament += 1;
            if (_actualTournament > _tournaments.Length - 1)
            {
                _actualTournament = _tournaments.Length - 1;
                _tournaments[_actualTournament].SetActive(true);
                _rightArrowTournament.interactable = _actualTournament != _tournaments.Length - 1;
                _leftArrowTournament.interactable = true;
            }
            else
            {
                _rightArrowTournament.interactable = _actualTournament != _tournaments.Length - 1;
                _leftArrowTournament.interactable = true;
                for (int i = _actualTournament - 1; i >= 0; i--)
                {
                    _tournaments[i].GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 1f);
                }

                _tournaments[_actualTournament].SetActive(true);
                _tournaments[_actualTournament].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 1f);
                for (int i = _actualTournament + 1; i < _tournaments.Length; i++)
                {
                    _tournaments[i].GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 1f);
                }
            }
        }

        public void OpenShop()
        {
            _tournamentCanvas.SetActive(false);
            _backgroundCanvas.gameObject.SetActive(true);
            _inventoryCanvas.gameObject.SetActive(false);
            _shopCanvas.gameObject.SetActive(true);
            _homeButton.interactable = true;
            _shopButton.interactable = false;
            _inventoryButton.interactable = true;
        }

        public void OpenInventory()
        {
            _tournamentCanvas.SetActive(false);
            _backgroundCanvas.gameObject.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(true);
            _shopCanvas.gameObject.SetActive(false);
            _homeButton.interactable = true;
            _shopButton.interactable = true;
            _inventoryButton.interactable = false;
        }

        public void CloseLockTournament()
        {
            _unlockTournament.gameObject.SetActive(false);
        }
    }
}