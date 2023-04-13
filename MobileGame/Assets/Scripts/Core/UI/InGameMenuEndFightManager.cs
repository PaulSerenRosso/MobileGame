using System.Collections;
using System.Collections.Generic;
using Service.Fight;
using TMPro;
using UnityEngine;

namespace Service.UI
{
    public class InGameMenuEndFightManager : MonoBehaviour
    {
        private IFightService _fightService;
        [SerializeField] private GameObject endFightPanel;
        [SerializeField] private TextMeshProUGUI endFightTitle;
        [SerializeField] private string endFightTitlePlayerVictoryName;
        [SerializeField] private string endFightTitlePlayerLoseName;
        public void Init(IFightService fightService)
        {
            _fightService = fightService; 
            endFightPanel.SetActive(false);
            _fightService.EndFightEvent += ActivateEndFightPanel;
        }

        private void ActivateEndFightPanel(bool isPlayerWin)
        {
            endFightPanel.SetActive(true);
            if (isPlayerWin) endFightTitle.text = endFightTitlePlayerVictoryName;
            else endFightTitle.text = endFightTitlePlayerLoseName;
        }

        public void BackToMainMenu()
        {
            _fightService.QuitFight();
            
        }

    }

}
