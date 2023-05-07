using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuManager : MonoBehaviour
    { 
        [Header("Menu UI")] 
        [SerializeField] private Button _playButton;
        [SerializeField] private GridLayoutGroup _enemySelectionGrid;
        [SerializeField] private GridLayoutGroup _environnementSelectionGrid;
        [SerializeField] private Button _environmentButton;
        [SerializeField] private Button _enemyButton;

        [SerializeField] private Canvas _topCanvas;
        [SerializeField] private Canvas _botCanvas;

        [SerializeField] private Button _homeButton;
        [SerializeField] private MenuTournamentManager _menuTournamentManager;
        [SerializeField] private GameObject _tournamentCanvas;
        
        [SerializeField] private Button _shopButton;
        [SerializeField] private Canvas _shopCanvas;
        
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Canvas _inventoryCanvas;
        

        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private string _nextEnvironmentAddressableName;
        private string _nextEnemyAddressableName;

        public void SetupMenu(IGameService gameService, ITournamentService tournamentService)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
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
            _shopCanvas.gameObject.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(false);
            _topCanvas.gameObject.SetActive(true);
            _botCanvas.gameObject.SetActive(true);
            _playButton.interactable = true;
            _homeButton.interactable = false;
            _shopButton.interactable = true;
            _inventoryButton.interactable = true;
        }
        
        public void StartFight()
        {
            _playButton.interactable = false;
            _gameService.LoadGameScene(_nextEnvironmentAddressableName, _nextEnemyAddressableName);
        }

        public void StartTournament()
        {
            _playButton.interactable = false;
            _tournamentCanvas.SetActive(false);
            _topCanvas.gameObject.SetActive(false);
            _botCanvas.gameObject.SetActive(false);
            _tournamentService.SetupTournament(_gameService, _gameService.GlobalSettingsSO.AllEnvironmentsSO, _gameService.GlobalSettingsSO.AllEnemyGlobalSO);
            _menuTournamentManager.SetupMenu(_gameService, _tournamentService, this);
            _menuTournamentManager.UpdateUITournament();
        }

        public void OpenShop()
        {
            _tournamentCanvas.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(false);
            _shopCanvas.gameObject.SetActive(true);
            _homeButton.interactable = true;
            _shopButton.interactable = false;
            _inventoryButton.interactable = true;
        }

        public void OpenInventory()
        {
            _tournamentCanvas.SetActive(false);
            _inventoryCanvas.gameObject.SetActive(true);
            _shopCanvas.gameObject.SetActive(false);
            _homeButton.interactable = true;
            _shopButton.interactable = true;
            _inventoryButton.interactable = false;
        }
    }
}