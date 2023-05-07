using DG.Tweening;
using Service.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuTournamentManager : MonoBehaviour
    {
        [SerializeField] private Button _playFightButton;
        [SerializeField] private Button _backFightButton;
        
        [SerializeField] private Canvas _tournamentButtonsCanvas;
        [SerializeField] private Canvas _tournamentQuarterCanvas;
        [SerializeField] private RectTransform _tournamentQuarterParent;
        [SerializeField] private Canvas _tournamentDemiCanvas;
        [SerializeField] private RectTransform _tournamentDemiParent;
        [SerializeField] private Canvas _tournamentFinalCanvas;
        [SerializeField] private RectTransform _tournamentFinalParent;
        
        private IGameService _gameService;
        private ITournamentService _tournamentService;
        private MenuManager _menuManager;
        private string _enemyAddressableName;
        private string _environmentAddressableName;
        
        public void SetupMenu(IGameService gameService, ITournamentService tournamentService, MenuManager menuManager)
        {
            _gameService = gameService;
            _tournamentService = tournamentService;
            _menuManager = menuManager;
        }

        public void UpdateUITournament()
        {
            _tournamentButtonsCanvas.gameObject.SetActive(true);
            Fight.Fight currentFight = _tournamentService.GetCurrentFight();
            switch (currentFight._tournamentState)
            {
                case TournamentState.QUARTER:
                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    break;
                case TournamentState.DEMI:
                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-1920, 0), 5f).OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(0, 0), 5f);
                    break;
                case TournamentState.FINAL:
                    _tournamentQuarterCanvas.gameObject.SetActive(true);
                    _tournamentDemiCanvas.gameObject.SetActive(true);
                    _tournamentFinalCanvas.gameObject.SetActive(true);
                    _tournamentQuarterParent.DOAnchorPos(new Vector2(-3840, 0), 5f).OnComplete(() => _tournamentQuarterCanvas.gameObject.SetActive(false));
                    _tournamentDemiParent.DOAnchorPos(new Vector2(-1920, 0), 5f).OnComplete(() => _tournamentDemiCanvas.gameObject.SetActive(false));
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
            _gameService.LoadGameScene(currentFight._environmentSO.EnvironmentAddressableName, currentFight._enemyGlobalSO.enemyAdressableName);
        }

        public void BackMenu()
        {
            DeactivateUITournament();
            _menuManager.ActivateHome();
        }
    }
}