using Service.Currency;
using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class InGameMenuEndFightManager : MonoBehaviour
    {
        [SerializeField] private GameObject endFightPanel;
        [SerializeField] private TextMeshProUGUI endFightTitle;
        [SerializeField] private string endFightTitlePlayerVictoryName;
        [SerializeField] private string endFightTitlePlayerLoseName;
        [SerializeField] private Button backToMainMenuButton;
        [SerializeField] private TextMeshProUGUI expAmountText;
        
        private ITournamentService _tournamentService;
        private IFightService _fightService;
        private ICurrencyService _currencyService;

        public void Init(IFightService fightService, ICurrencyService currencyService, ITournamentService tournamentService)
        {
            _fightService = fightService;
            _tournamentService = tournamentService;
            _currencyService = currencyService;
            if (!fightService.GetFightTutorial()) _fightService.EndFightEvent += ActivateEndFightPanel;
        }

        private void ActivateEndFightPanel(bool isPlayerWin)
        {
            endFightPanel.SetActive(true);
            int expAmount = 0;
            expAmountText.text = "+" + expAmount;
            if (isPlayerWin)
            {
                if (!_fightService.GetFightTutorial() && !_fightService.GetFightDebug())
                {
                    expAmount = _tournamentService.GetSettings()
                        .ExpAmountWhenWinStepTournament[(int)_tournamentService.GetCurrentFightPlayer().TournamentStep];
                    expAmountText.text = "+" + expAmount;
                    _currencyService.AddXP(expAmount);
                }
                // endFightTitle.text = endFightTitlePlayerVictoryName;
            }
            else
            {
                if (!_fightService.GetFightTutorial() && !_fightService.GetFightDebug())
                {
                    expAmount = _tournamentService.GetSettings()
                        .ExpAmountWhenLoseStepTournament[
                            (int)_tournamentService.GetCurrentFightPlayer().TournamentStep];
                    expAmountText.text = "+" + expAmount;
                    _currencyService.AddXP(expAmount);
                }
                // endFightTitle.text = endFightTitlePlayerLoseName;
            }
        }

        public void BackToMainMenu()
        {
            backToMainMenuButton.interactable = false;
            _fightService.QuitFight();
        }
    }
}