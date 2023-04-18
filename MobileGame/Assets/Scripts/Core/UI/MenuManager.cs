using UnityEngine;
using DG.Tweening;
using Service.Fight;
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
        private string _nextEnvironmentName;
        public void SetupMenu(IGameService gameService, string nextEnvironmentName)
        {
            _settingsMenu.transform.DOScale(0f, 0f).SetEase(Ease.OutBack);
            _nextEnvironmentName = nextEnvironmentName;
            _gameService = gameService;
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);
            // _reportButton.onClick.AddListener();
        }

        public void StartFight()
        {
            Debug.Log("test");
            _playButton.interactable = false;
           _gameService.LoadGameScene(_nextEnvironmentName);
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