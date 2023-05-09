using Service.Fight;
using TMPro;
using UnityEngine;

namespace Service.UI
{
    public class InGameMenuRoundManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _roundTitle;

        public void Init(IFightService fightService)
        {
            fightService.InitiateRoundEvent += ActivateChangeRound;
            fightService.EndInitiateRoundEvent += DeactivateChangeRound;
        }

        private void ActivateChangeRound(int roundCount)
        {
            _roundTitle.gameObject.SetActive(true);
            _roundTitle.text = $"Round {roundCount + 1}";
        }

        private void DeactivateChangeRound()
        {
            _roundTitle.gameObject.SetActive(false);
        }
    }
}