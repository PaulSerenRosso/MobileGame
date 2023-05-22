using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Service.Currency;
using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuTournamentManager : MonoBehaviour
    {
        [SerializeField] private Button _playFightButton;
        [SerializeField] private Button _backMenuButton;

        [SerializeField] private Canvas _tournamentButtonsCanvas;
        [SerializeField] private Canvas _tournamentQuarterCanvas;
        [SerializeField] private RectTransform _tournamentQuarterParent;
        [SerializeField] private Canvas _tournamentDemiCanvas;
        [SerializeField] private RectTransform _tournamentDemiParent;
        [SerializeField] private Canvas _tournamentFinalCanvas;
        [SerializeField] private RectTransform _tournamentFinalParent;

        [SerializeField] private Image[] _imageQuarterWinners;
        [SerializeField] private Image[] _imageDemiWinners;
        [SerializeField] private Image[] _imageFinalWinners;

        [SerializeField] private TextMeshProUGUI _quarterName;
        [SerializeField] private TextMeshProUGUI[] _demiNames;
        [SerializeField] private TextMeshProUGUI[] _finalNames;

        [SerializeField] private TextMeshProUGUI[] _quarterFakeNames;
        [SerializeField] private TextMeshProUGUI[] _demiFakeNames;

        [SerializeField] private Canvas _pubCanvas;
        [SerializeField] private Canvas _winTournament;
        [SerializeField] private Canvas _defeatTournament;
        [SerializeField] private TextMeshProUGUI _winTournamentText;
        
        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private Fight.Fight[] _fights;
        private List<string> _fakeNames;
        private MenuManager _menuManager;
        private string _enemyAddressableName;
        private ICurrencyService _currencyService;
        private string _environmentAddressableName;

        public void SetupMenu(IGameService gameService, ITournamentService tournamentService, MenuManager menuManager,
            ICurrencyService currencyService)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
            _menuManager = menuManager;
            if (!_tournamentService.GetTournamentIsActive()) _tournamentService.SetTournament();
            _fights = _tournamentService.GetFights();
            _fakeNames = _tournamentService.GetFakeNames();
            SetTournamentNames();
            UpdateCurrentFightUI();
            if (_tournamentService.CompareState(FightState.DEFEAT)) _defeatTournament.gameObject.SetActive(true);
            else if (_tournamentService.CompareState(FightState.WAITING)) UpdateCurrentFightUI();
            else ActivateWinnerUI();
            _currencyService = currencyService;
        }

        private void SetTournamentNames()
        {
            _quarterName.text = _fights[0].EnemyGlobalSO.Name;
            foreach (var demiName in _demiNames)
            {
                demiName.text = _fights[1].EnemyGlobalSO.Name;
            }

            foreach (var finalName in _finalNames)
            {
                finalName.text = _fights[2].EnemyGlobalSO.Name;
            }

            for (var index = 0; index < _quarterFakeNames.Length; index++)
            {
                var quarterFakeName = _quarterFakeNames[index];
                quarterFakeName.text = _fakeNames[index];
            }

            foreach (var demiFakeName in _demiFakeNames)
            {
                demiFakeName.text = _fakeNames.LastOrDefault();
            }
        }

        public void UpdateUITournament()
        {
            _tournamentButtonsCanvas.gameObject.SetActive(true);
            switch (_tournamentService.GetCurrentFightPlayer().TournamentStep)
            {
                case TournamentStep.QUARTER :
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(0, 0), 0f);
                    _tournamentDemiParent.DOAnchorPos(new Vector2(1920, 0), 0f);
                    _tournamentFinalParent.DOAnchorPos(new Vector2(3840, 0), 0f);
                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    break;
                case TournamentStep.DEMI :
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-1920, 0), 0f);
                    _tournamentDemiParent.DOAnchorPos(new Vector2(0, 0), 0f);
                    _tournamentFinalParent.DOAnchorPos(new Vector2(1920, 0), 0f);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    break;
                case TournamentStep.FINAL :
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 0f);
                    _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 0f);
                    _tournamentFinalParent.DOAnchorPos(new Vector2(0, 0), 0f);
                    _tournamentFinalCanvas.gameObject.SetActive(true);
                    break;
            }
            
        }

        private async void UpdateCurrentFightUI()
        {
            Fight.Fight currentFight = _tournamentService.GetCurrentFightPlayer();
            switch (currentFight.TournamentStep)
            {
                case TournamentStep.DEMI:
                    _playFightButton.interactable = false;
                    _backMenuButton.interactable = false;
                    foreach (var imageQuarterWinner in _imageQuarterWinners)
                    {
                        imageQuarterWinner.color = Color.green;
                    }

                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    await UniTask.Delay(2000);
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                        .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(0, 0), 5f)
                        .OnComplete(ActivateButtons);
                    break;

                case TournamentStep.FINAL:
                    _playFightButton.interactable = false;
                    _backMenuButton.interactable = false;
                    foreach (var imageQuarterWinner in _imageQuarterWinners)
                    {
                        imageQuarterWinner.color = Color.green;
                    }

                    foreach (var imageDemiWinner in _imageDemiWinners)
                    {
                        imageDemiWinner.color = Color.green;
                    }

                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    _tournamentFinalCanvas.gameObject.SetActive(true);
                    await UniTask.Delay(2000);
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 5f)
                        .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                        .OnComplete(() => _tournamentDemiCanvas.gameObject.SetActive(false));
                    _tournamentFinalParent.DOAnchorPos(new Vector2(0, 0), 5f)
                        .OnComplete(ActivateButtons);
                    break;
            }
        }

        private async void ActivateWinnerUI()
        {
            _playFightButton.interactable = false;
            foreach (var imageQuarterWinner in _imageQuarterWinners)
            {
                imageQuarterWinner.color = Color.green;
            }

            foreach (var imageDemiWinner in _imageDemiWinners)
            {
                imageDemiWinner.color = Color.green;
            }

            foreach (var imageFinalWinner in _imageFinalWinners)
            {
                imageFinalWinner.color = Color.green;
            }

            _tournamentQuarterCanvas.gameObject.SetActive(true);
            _tournamentDemiCanvas.gameObject.SetActive(true);
            _tournamentFinalCanvas.gameObject.SetActive(true);
            await UniTask.Delay(2000);
            _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 5f)
                .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
            _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                .OnComplete(() => _tournamentDemiCanvas.gameObject.SetActive(false));
            _tournamentFinalParent.DOAnchorPos(new Vector2(0, 0), 5f)
                .OnComplete(ActivateWinTournamentCanvas);
        }

        private void ActivateWinTournamentCanvas()
        {
            ActivateButtons();
            _winTournamentText.text = "+" + _tournamentService.GetSettings().CoinsAmountWhenWinTournament;
            _winTournament.gameObject.SetActive(true);
        }

        private void ActivateButtons()
        {
            _playFightButton.interactable = true;
            _backMenuButton.interactable = true;
        }

        private void GainEndTournamentCoins()
        {
            _currencyService.AddCoins(_tournamentService.GetSettings().CoinsAmountWhenWinTournament);
        }

        public void DeactivateUITournament()
        {
            _tournamentButtonsCanvas.gameObject.SetActive(false);
            _tournamentQuarterCanvas.gameObject.SetActive(false);
            _tournamentDemiCanvas.gameObject.SetActive(false);
            _tournamentFinalCanvas.gameObject.SetActive(false);
        }

        public void StartFight()
        {
            _playFightButton.interactable = false;
            Fight.Fight currentFight = _tournamentService.GetCurrentFightPlayer();
            _gameService.LoadGameScene(currentFight.EnvironmentSO.EnvironmentAddressableName,
                currentFight.EnemyGlobalSO.enemyAdressableName, false, false);
        }

        public void QuitTournament()
        {
            _defeatTournament.gameObject.SetActive(false);
            _winTournament.gameObject.SetActive(false);
            _tournamentService.ResetTournament();
            GainEndTournamentCoins();
            BackMenu();
        }

        public void BackMenu()
        {
            DeactivateUITournament();
            _menuManager.ActivateHome();
        }

        public void LaunchPub()
        {
            _defeatTournament.gameObject.SetActive(false);
            _tournamentService.GetCurrentFightPlayer().FightState = FightState.WAITING;
            _pubCanvas.gameObject.SetActive(true);
        }

        public void ClosePub()
        {
            _pubCanvas.gameObject.SetActive(false);
            UpdateUITournament();
        }
    }
}