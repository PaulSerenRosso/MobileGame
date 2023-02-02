using UnityEngine;
using DG.Tweening;
using Service;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _openSettingsButton;
    [SerializeField] private Image _settingsMenu;
    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private Button _reportButton;
    [SerializeField] private string _sceneToLoad;

    private void Awake()
    {
        _settingsMenu.transform.DOScale(0f, 0f).SetEase(Ease.OutBack);
    }

    public void SetupMenu(IGameService gameService)
    {
        if (_sceneToLoad != null) _playButton.onClick.AddListener(() => gameService.StartGame());
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