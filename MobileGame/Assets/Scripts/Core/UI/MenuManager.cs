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

        public void SetupMenu(IFightService fightService, string nextEnvironmentName)
        {
            _settingsMenu.transform.DOScale(0f, 0f).SetEase(Ease.OutBack);
            if (_sceneToLoad != null) _playButton.onClick.AddListener(() => fightService.StartFight(nextEnvironmentName));
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);
            // _reportButton.onClick.AddListener();
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