using System.Collections;
using System.Collections.Generic;
using Service.Currency;
using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class InGameMenuEndFightManager : MonoBehaviour
    {
        private IFightService _fightService;
        [SerializeField] private GameObject endFightPanel;
        [SerializeField] private TextMeshProUGUI endFightTitle;
        [SerializeField] private string endFightTitlePlayerVictoryName;
        [SerializeField] private string endFightTitlePlayerLoseName;
        [SerializeField] private Button backToMainMenuButton;
        public void Init(IFightService fightService, ICurrencyService currencyService, ITournamentService tournamentService)
        {
            _fightService = fightService; 
            endFightPanel.SetActive(false);
            if (!fightService.GetFightTutorial()) _fightService.EndFightEvent += ActivateEndFightPanel;
        }

        private void ActivateEndFightPanel(bool isPlayerWin)
        {
            endFightPanel.SetActive(true);
            if (isPlayerWin) endFightTitle.text = endFightTitlePlayerVictoryName;
            else endFightTitle.text = endFightTitlePlayerLoseName;
        }

        public void BackToMainMenu()
        {
            backToMainMenuButton.interactable = false;
            _fightService.QuitFight();
        }
    }
}