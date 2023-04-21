using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuManager : MonoBehaviour
    { 
        [Header("Menu UI")] 
        [SerializeField] private Button _playButton;

        [Header("Settings UI")] 
        [SerializeField] private Button _openSettingsButton;

        [SerializeField] private Image _settingsMenu;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private Button _reportButton;
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private GridLayoutGroup _enemySelectionGrid;
        [SerializeField] private GridLayoutGroup _environnementSelectionGrid;

        private IGameService _gameService;
        private string _nextEnvironmentAddressableName;
        private string _nextEnemyAddressableName;

        [FormerlySerializedAs("environnementButton")] [SerializeField] private Button _environnementButton;
        [FormerlySerializedAs("enemyButton")] [SerializeField] private Button _enemyButton;

        public void SetupMenu(IGameService gameService)
        {
            _settingsMenu.transform.DOScale(0f, 0f).SetEase(Ease.OutBack);
            _gameService = gameService;
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);
            foreach (var enemyGlobalSo in gameService.GlobalSettingsSO.AllEnemyGlobalSO)
            {
                var button = Instantiate(_enemyButton, _enemySelectionGrid.transform);
                button.onClick.AddListener(() => SetEnemyAddressableName(enemyGlobalSo.enemyAdressableName));
                button.image.sprite = enemyGlobalSo.Sprite;
                button.GetComponentInChildren<TextMeshProUGUI>().text = enemyGlobalSo.Name;
            }

            foreach (var environmentSo in gameService.GlobalSettingsSO.AllEnvironmentsSO)
            {
                var button = Instantiate(_environnementButton, _environnementSelectionGrid.transform);
                button.onClick.AddListener(
                    () => SetEnvironmentAddressableName(environmentSo.EnvironmentAddressableName));
                button.image.sprite = environmentSo.EnvironmentSprite;
                button.GetComponentInChildren<TextMeshProUGUI>().text = environmentSo.Name;
            }
        }

        public void SetEnvironmentAddressableName(string value) => _nextEnvironmentAddressableName = value;
        public void SetEnemyAddressableName(string value) => _nextEnemyAddressableName = value;

        public void StartFight()
        {
            _playButton.interactable = false;
            _gameService.LoadGameScene(_nextEnvironmentAddressableName, _nextEnemyAddressableName);
        }

        private void OpenSettings()
        {
            _openSettingsButton.transform.DOKill();
            _openSettingsButton.transform.DOScale(0f, 0.25f).SetEase(Ease.OutBack);
            _settingsMenu.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        }

        private void CloseSettings()
        {
            _settingsMenu.transform.DOKill();
            _settingsMenu.transform.DOScale(0f, 0.25f).SetEase(Ease.OutBack);
            _openSettingsButton.transform.DOKill();
            _openSettingsButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        }
    }
}