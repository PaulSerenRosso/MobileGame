using System;
using HelperPSR.MonoLoopFunctions;
using Service.Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class InGameMenuRoundManager : MonoBehaviour, IUpdatable
    {
        [SerializeField] private TextMeshProUGUI _roundTitle;
        [SerializeField] private TextMeshProUGUI _countdownRound;
        [SerializeField] private int _countdownTimer;
        [SerializeField] private Image[] _roundCountImagePlayer;
        [SerializeField] private Image[] _roundCountImageEnemy;
        [SerializeField] private Sprite _neutralRound;
        [SerializeField] private Sprite _playerRound;
        [SerializeField] private Sprite _enemyRound;

        private float _timer;
        private IFightService _fightService;
        
        public void Init(IFightService fightService)
        {
            _fightService = fightService;
            _fightService.InitiateRoundEvent += ActivateChangeRound;
            _fightService.EndInitiateRoundEvent += DeactivateChangeRound;
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
            switch (_fightService.GetEnemyRoundCount())
            {
                case 0:
                    foreach (var roundCountEnemyImage in _roundCountImageEnemy)
                    {
                        roundCountEnemyImage.sprite = _neutralRound;
                    }
                    break;
                case 1:
                    _roundCountImageEnemy[0].sprite = _enemyRound;
                    break;
                case 2:
                    foreach (var roundCountEnemyImage in _roundCountImageEnemy)
                    {
                        roundCountEnemyImage.sprite = _enemyRound;
                    }
                    break;
            }
            switch (_fightService.GetPlayerRoundCount())
            {
                case 0:
                    foreach (var roundCountPlayerImage in _roundCountImagePlayer)
                    {
                        roundCountPlayerImage.sprite = _neutralRound;
                    }
                    break;
                case 1:
                    _roundCountImagePlayer[0].sprite = _playerRound;
                    break;
                case 2:
                    foreach (var roundCountPlayerImage in _roundCountImagePlayer)
                    {
                        roundCountPlayerImage.sprite = _playerRound;
                    }
                    break;
            }
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