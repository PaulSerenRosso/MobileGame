using UnityEngine;
using DG.Tweening;
using Service.Fight;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI")] [SerializeField] private Button _playButton;

        [Header("Settings UI")] [SerializeField]
        private Button _openSettingsButton;

        [SerializeField] private Image _settingsMenu;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private Button _reportButton;
        [SerializeField] private string _sceneToLoad;
        private IGameService _gameService;
        private string _nextEnvironmentAddressableName;
        private string _nextEnemyAddressableName;
        [SerializeField] private GridLayout _enemySelectionGrid;
        [SerializeField] private GridLayout _environnementSelectionGrid;
        [FormerlySerializedAs("environnementButton")] [SerializeField] private Button _environnementButton;
        [FormerlySerializedAs("enemyButton")] [SerializeField] private Button _enemyButton;
        public void SetupMenu(IGameService gameService)
        {
            _settingsMenu.transform.DOScale(0f, 0f).SetEase(Ease.OutBack);
            _gameService = gameService;
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);
            foreach (var enemyMacroSo in gameService.GlobalSettingsSO.AllEnemyMacroSO)
            {
                var button = Instantiate(_enemyButton, _enemySelectionGrid.transform);
                button.onClick.AddListener(()=>SetEnemyAddressableName(enemyMacroSo.enemyAdressableName));
                button.image.sprite = enemyMacroSo.Sprite;
            }
            foreach (var environmentMicroSo in gameService.GlobalSettingsSO.AllEnvironmentsSO)
            {
                var button = Instantiate(_environnementButton, _environnementSelectionGrid.transform);
                button.onClick.AddListener(()=>SetEnvironmentAddressableName(environmentMicroSo.EnvironmentAddressableName));
                button.image.sprite = environmentMicroSo.EnvironmentSprite;
            }
        }

        public void SetEnvironmentAddressableName(string value) => _nextEnvironmentAddressableName = value;
        public void SetEnemyAddressableName(string value) => _nextEnemyAddressableName = value;

        public void StartFight()
        {
            Debug.Log("test");
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