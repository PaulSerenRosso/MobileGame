using DG.Tweening;
using HelperPSR.MonoLoopFunctions;
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

        [SerializeField] private GameObject _friendsPanel;
        [SerializeField] private GameObject _friendPopup;
        [SerializeField] private GameObject _dailyPanel;
        [SerializeField] private GameObject _friendsLayout;
        [SerializeField] private Button _friendTag;
        [SerializeField] private Image _friendPopupImage;
        [SerializeField] private TextMeshProUGUI _friendPopupNameText;

        [SerializeField] private DragPlayer _dragPlayer;
        
        private TournamentSettingsSO _tournamentSettingsSO;
        private int _actualTournament;
        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private IFightService _fightService;
        private string _nextEnvironmentAddressableName;
        private string _nextEnemyAddressableName;
        private ICurrencyService _currencyService;
        private GameObject _player;

        public void SetupMenu(IGameService gameService, ITournamentService tournamentService,
            ICurrencyService currencyService, IItemsService itemsService, IShopService shopService,
            GameObject player, IFightService fightService)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
            _dragPlayer.Init(player);
            _tournamentSettingsSO = _tournamentService.GetSettings();
            _fightService = fightService;
            _menuTopManager.SetUp(currencyService);
            _currencyService = currencyService;
            _player = player;
            _menuInventoryManager.Setup(itemsService, player.GetComponentInChildren<PlayerItemsLinker>());
            _menuShopManager.Setup(itemsService, currencyService, shopService);
            _tournaments[_actualTournament].SetActive(true);
            _leftArrowTournament.interactable = false;
            _homeButton.interactable = false;
            _menuTournamentManager.SetupMenu(_gameService, _tournamentService, this, _currencyService);
            if (!_fightService.GetFightTutorial() && !_fightService.GetFightDebug())
            {
                _playTournamentButton.interactable = false;
                _tournamentCanvas.SetActive(false);        
                _topCanvas.gameObject.SetActive(false);    
                _botCanvas.gameObject.SetActive(false);    
                _menuTournamentManager.OpenUITournament();
            }

            foreach (var enemyGlobalSo in gameService.GlobalSettingsSO.AllEnemyGlobalSO)
            {
                if (enemyGlobalSo.enemyAdressableName.Contains("Tutorial")) continue;
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

            foreach (var fakeName in _tournamentSettingsSO.FakeNames)
            {
                var button = Instantiate(_friendTag, _friendsLayout.transform);
                button.onClick.AddListener(() => OpenFriendPopup(fakeName.picture, fakeName.name));
                button.GetComponent<FriendComponent>().SetValue(fakeName.picture,fakeName.name);
            }
        }

        public void SetEnvironmentAddressableName(string value) => _nextEnvironmentAddressableName = value;

        public void SetEnemyAddressableName(string value) => _nextEnemyAddressableName = value;

        public void ActivateHome()
        {
            _player.SetActive(false); 
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
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 1f);
                }

                _tournaments[_actualTournament].SetActive(true);
                _tournaments[_actualTournament].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 2f);
                for (int i = _actualTournament + 1; i < _tournaments.Length; i++)
                {
                    _tournaments[i].GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1920 * (i - _actualTournament), 0), 1f);
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
            
            _player.SetActive(false);
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
            _player.SetActive(true);
            _tournamentCanvas.SetActive(false);
            _backgroundCanvas.gameObject.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(true);
            _shopCanvas.gameObject.SetActive(false);
            _homeButton.interactable = true;
            _shopButton.interactable = true;
            _inventoryButton.interactable = false;
            _menuInventoryManager.UpdateScrollRect();
        }

        public void OpenDaily()
        {
            _dailyPanel.SetActive(true);
        }

        public void OpenFriends()
        {
            _friendsPanel.SetActive(true);
        }

        private void OpenFriendPopup(Sprite picture, string name)
        {
            _friendPopupImage.sprite = picture;
            _friendPopupNameText.text = name;
            _friendPopup.SetActive(true);
        }

        public void CloseLockTournament()
        {
            _unlockTournament.gameObject.SetActive(false);
        }

        public void ClosePopup()
        {
            _friendsPanel.SetActive(false);
            _dailyPanel.SetActive(false);
        }

        public void CloseFriendPopup()
        {
            _friendPopup.SetActive(false);
        }
    }
}