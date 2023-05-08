using DG.Tweening;
using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuTournamentManager : MonoBehaviour
    {
        [SerializeField] private Button _playFightButton;

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

        [SerializeField] private TextMeshProUGUI[] _quarterPlayerNames;
        [SerializeField] private TextMeshProUGUI _quarterName;
        [SerializeField] private TextMeshProUGUI[] _demiNames;
        [SerializeField] private TextMeshProUGUI[] _finalNames;

        [SerializeField] private Canvas _winTournament;

        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private Fight.Fight[] _fights;
        private MenuManager _menuManager;
        private string _enemyAddressableName;
        private string _environmentAddressableName;

        public void SetupMenu(IGameService gameService, ITournamentService tournamentService, MenuManager menuManager)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
            _menuManager = menuManager;
            _fights = _tournamentService.GetFights();
            _quarterName.text = _fights[0]._enemyGlobalSO.Name;
            foreach (var demiName in _demiNames)
            {
                demiName.text = _fights[1]._enemyGlobalSO.Name;
            }

            foreach (var finalName in _finalNames)
            {
                finalName.text = _fights[2]._enemyGlobalSO.Name;
            }
        }

        public void UpdateUITournament()
        {
            _tournamentButtonsCanvas.gameObject.SetActive(true);
            Fight.Fight currentFight = _tournamentService.GetCurrentFight();
            if (_tournamentService.GetStateTournament())
            {
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
                _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 5f)
                    .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                    .OnComplete(() => _tournamentDemiCanvas.gameObject.SetActive(false));
                _tournamentFinalParent.DOAnchorPos(new Vector2(0, 0), 5f)
                    .OnComplete(() => _winTournament.gameObject.SetActive(true));
            }

            if (currentFight == null) return;
            switch (currentFight._tournamentState)
            {
                case TournamentState.QUARTER:
                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    break;
                case TournamentState.DEMI:
                    foreach (var imageQuarterWinner in _imageQuarterWinners)
                    {
                        imageQuarterWinner.color = Color.green;
                    }

                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                        .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(0, 0), 5f);
                    break;
                case TournamentState.FINAL:
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
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 5f)
                        .OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 5f)
                        .OnComplete(() => _tournamentDemiCanvas.gameObject.SetActive(false));
                    _tournamentFinalParent.DOAnchorPos(new Vector2(0, 0), 5f);
                    break;
            }
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
            Fight.Fight currentFight = _tournamentService.GetCurrentFight();
            _gameService.LoadGameScene(currentFight._environmentSO.EnvironmentAddressableName,
                currentFight._enemyGlobalSO.enemyAdressableName);
        }

        public void QuitTournament()
        {
            _tournamentService.ResetTournament();
            BackMenu();
        }

        public void BackMenu()
        {
            DeactivateUITournament();
            _menuManager.ActivateHome();
        }
    }
}