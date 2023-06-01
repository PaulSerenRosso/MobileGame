using HelperPSR.MonoLoopFunctions;
using Service.Fight;
using TMPro;
using UnityEngine;

namespace Service.UI
{
    public class InGameMenuRoundManager : MonoBehaviour, IUpdatable
    {
        [SerializeField] private TextMeshProUGUI _roundTitle;
        [SerializeField] private TextMeshProUGUI _countdownRound;
        [SerializeField] private int _countdownTimer;

        private float _timer;
        
        public void Init(IFightService fightService)
        {
            fightService.InitiateRoundEvent += ActivateChangeRound;
            fightService.EndInitiateRoundEvent += DeactivateChangeRound;
        }

        public void OnUpdate()
        {
            _timer -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(_timer % 60);
            if (seconds < 0) return;
            _countdownRound.text = seconds.ToString();
        }

        private void ActivateChangeRound(int roundCount)
        {
            _timer = _countdownTimer;
            _roundTitle.gameObject.SetActive(true);
            _roundTitle.text = roundCount == -1 ? "Start training" : $"Round {roundCount + 1}";
            _countdownRound.gameObject.SetActive(true);
            _countdownRound.text = _countdownTimer.ToString();
            UpdateManager.Register(this);
        }

        private void DeactivateChangeRound()
        {
            _roundTitle.gameObject.SetActive(false);
            _countdownRound.gameObject.SetActive(false);
            UpdateManager.UnRegister(this);
        }
    }
}