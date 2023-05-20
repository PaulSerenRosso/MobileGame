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
        private ITournamentService _tournamentService;
        [SerializeField] private TextMeshProUGUI expAmountText;
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
            if (isPlayerWin)
            {
                int expAmount = _tournamentService.GetSettings()
                    .ExpAmountWhenWinStepTournament[(int)_tournamentService.GetCurrentFightPlayer().FightState];
                expAmountText.text = "+"+ expAmount.ToString();
                _currencyService.AddXP(expAmount);
                //endFightTitle.text = endFightTitlePlayerVictoryName;
            }
            else
            {
                int expAmount = _tournamentService.GetSettings()
                    .ExpAmountWhenLoseStepTournament[(int)_tournamentService.GetCurrentFightPlayer().FightState];
                expAmountText.text = "+"+ expAmount.ToString();
                _currencyService.AddXP(expAmount);
                //endFightTitle.text = endFightTitlePlayerLoseName;
            }
        }

        public void BackToMainMenu()
        {
            backToMainMenuButton.interactable = false;
            _fightService.QuitFight();
        }
    }
}