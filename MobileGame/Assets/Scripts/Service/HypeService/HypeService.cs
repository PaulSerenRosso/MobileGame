using System;
using Addressables;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using UnityEngine;

namespace Service.Hype
{
    public class HypeService : IHypeService, IRemoteConfigurable, IUpdatable
    {
        private EnemyManager _enemyManager;
        private Hype _hypePlayer;
        private Hype _hypeEnemy;
        private HypeSO _playerHypeSO;
        private HypeSO _enemyHypeSO;
        private HypeServiceSO _hypeServiceSo;
        private bool _isDecreasePlayerHype;
        private float _decreasePlayerHypeTimer;
        private bool _inCooldown;

        public void IncreaseHypePlayer(float amount)
        {
            IncreaseHype(amount, _hypePlayer, _hypeEnemy);
        }

        public void IncreaseHypeEnemy(float amount)
        {
            IncreaseHype(amount, _hypeEnemy, _hypePlayer);
        }

        public void IncreaseHype(float amount, Hype hype, Hype otherHype)
        {
            if (CheckMaximumHypeIsReached(hype.CurrentValue)) return;
            if (CheckMaximumHypeIsReached(hype.CurrentValue + amount))
            {
                hype.CurrentValue = _hypeServiceSo.MaxHype;
                ReachMaximumHypeEvent?.Invoke(amount);
            }
            else
            {
                if (CheckHypeReachOtherHype(hype, amount, otherHype))
                {
                  
                    TryGainUltimateValue(hype, hype.CurrentValue - _hypeServiceSo.MaxHype - otherHype.CurrentValue);
                    hype.CurrentValue = _hypeServiceSo.MaxHype - otherHype.CurrentValue;
                }
                else
                {
                    TryGainUltimateValue(hype, amount);
                    hype.CurrentValue += amount;
                }
            }

            hype.IncreaseHypeEvent?.Invoke(amount);
        }

        private void TryGainUltimateValue(Hype hype, float amount)
        {
            if (!hype.isInUltimateArea)
            {
                if (hype.CurrentValue + amount >= hype.HypeSo.UltimateValue)
                {
                    hype.isInUltimateArea = true;
                    hype.GainUltimateEvent?.Invoke(amount);
                }
            }
        }

        private void TryLoseUltimateValue(Hype hype, float amount)
        {
            if (hype.isInUltimateArea)
            {
                if (hype.CurrentValue - amount < hype.HypeSo.UltimateValue)
                {
                    hype.isInUltimateArea = false;
                    hype.LoseUltimateEvent?.Invoke(amount);
                }
            }
        }

        public void DecreaseHypePlayer(float amount)
        {
            DecreaseHype(amount, _hypePlayer);
        }

        public void DecreaseHypeEnemy(float amount)
        {
            DecreaseHype(amount, _hypeEnemy);
        }

        public void DecreaseHype(float amount, Hype hype)
        {
            if (CheckMinimumHypeIsReached(hype.CurrentValue)) return;
            if (CheckMinimumHypeIsReached(hype.CurrentValue - amount))
            {
                hype.CurrentValue = 0;
                ReachMinimumHypeEvent?.Invoke(amount);
            }
            else
            {
                TryLoseUltimateValue(hype, amount);
                hype.CurrentValue -= amount;
            }

            hype.DecreaseHypeEvent?.Invoke(amount);
        }

        public void SetHypePlayer(float value)
        {
            SetHype(value, _hypePlayer);
        }

        public void SetHypeEnemy(float value)
        {
            SetHype(value, _hypeEnemy);
        }

        public void ResetHypePlayer()
        {
            SetHypePlayer(_hypeServiceSo.PlayerHypeSO.StartValue);
            _hypePlayer.isInUltimateArea = false;
            _hypePlayer.LoseUltimateEvent?.Invoke(_hypeServiceSo.PlayerHypeSO.StartValue);
            
        }

        public void ResetHypeEnemy()
        {
            SetHypeEnemy(_hypeServiceSo.EnemyHypeSO.StartValue);
            _hypeEnemy.isInUltimateArea = false;
            _hypeEnemy.LoseUltimateEvent?.Invoke(_hypeServiceSo.EnemyHypeSO.StartValue);
        }

        public void ActivateDecreaseUpdateHypePlayer()
        {
            UpdateManager.Register(this);
        }

        public void DeactivateDecreaseUpdateHypePlayer()
        {
            UpdateManager.UnRegister(this);
        }

        private void SetHype(float value, Hype hype)
        {
            if (!CheckHypeIsClamped(value)) return;
            hype.CurrentValue = value;

            hype.SetHypeEvent?.Invoke(value);
        }

        public float GetCurrentHypePlayer()
        {
            return _hypePlayer.CurrentValue;
        }

        public float GetCurrentHypeEnemy()
        {
            return _hypeEnemy.CurrentValue;
        }

        public float GetMaximumHype()
        {
            return _hypeServiceSo.MaxHype;
        }

        public Action<float> GetPlayerIncreaseHypeEvent
        {
            get => _hypePlayer.IncreaseHypeEvent;
            set => _hypePlayer.IncreaseHypeEvent = value;
        }

        public Action<float> GetPlayerDecreaseHypeEvent
        {
            get => _hypePlayer.DecreaseHypeEvent;
            set => _hypePlayer.DecreaseHypeEvent = value;
        }

        public Action<float> GetPlayerSetHypeEvent
        {
            get => _hypePlayer.SetHypeEvent;
            set => _hypePlayer.SetHypeEvent = value;
        }

        public Action<float> GetPlayerGainUltimateEvent
        {
            get => _hypePlayer.GainUltimateEvent;
            set => _hypePlayer.GainUltimateEvent = value;
        }

        public Action<float> GetPlayerLoseUltimateEvent
        {
            get => _hypePlayer.LoseUltimateEvent;
            set => _hypePlayer.LoseUltimateEvent = value;
        }

        public Action<float> GetEnemyIncreaseHypeEvent
        {
            get => _hypeEnemy.IncreaseHypeEvent;
            set => _hypeEnemy.IncreaseHypeEvent = value;
        }

        public Action<float> GetEnemyDecreaseHypeEvent
        {
            get => _hypeEnemy.DecreaseHypeEvent;
            set => _hypeEnemy.DecreaseHypeEvent = value;
        }

        public Action<float> GetEnemySetHypeEvent
        {
            get => _hypeEnemy.SetHypeEvent;
            set => _hypeEnemy.SetHypeEvent = value;
        }

        public Action<float> GetEnemyGainUltimateEvent
        {
            get => _hypeEnemy.GainUltimateEvent;
            set => _hypeEnemy.GainUltimateEvent = value;
        }

        public Action<float> GetEnemyLoseUltimateEvent
        {
            get => _hypeEnemy.LoseUltimateEvent;
            set => _hypeEnemy.LoseUltimateEvent = value;
        }
        
        public float GetMinimumHype()
        {
            return 0;
        }

        private bool CheckHypeIsClamped(float value)
        {
            return value <= _hypeServiceSo.MaxHype && value >= 0;
        }

        private bool CheckHypeReachOtherHype(Hype hype, float amount, Hype otherHype)
        {
            return ((hype.CurrentValue + amount + otherHype.CurrentValue) >= _hypeServiceSo.MaxHype);
        }

        private bool CheckMaximumHypeIsReached(float currentHype)
        {
            return currentHype >= _hypeServiceSo.MaxHype;
        }

        private bool CheckMinimumHypeIsReached(float currentHype)
        {
            return currentHype <= 0;
        }

        public event Action<float> ReachMaximumHypeEvent;
        public event Action<float> ReachMinimumHypeEvent;


        public void EnabledService()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<HypeServiceSO>("HypeSO", SetHypeSO);
        }

        private void DecreaseHypeInUpdate()
        {
            DecreaseHype(_hypeServiceSo.AmountPlayerHypeDecrease * Time.deltaTime, _hypePlayer);
        }

        private void SetHypeSO(HypeServiceSO hypeServiceSo)
        {
            _hypeServiceSo = hypeServiceSo;
            _hypeEnemy = new Hype();
            _hypePlayer = new Hype();
            RemoteConfigManager.RegisterRemoteConfigurable(this);
            ResetHypePlayer();
            _hypePlayer.HypeSo = _hypeServiceSo.PlayerHypeSO;
            ResetHypeEnemy();
            _hypeEnemy.HypeSo = _hypeServiceSo.EnemyHypeSO;
            _isDecreasePlayerHype = false;
            _decreasePlayerHypeTimer = 0;
            _hypePlayer.IncreaseHypeEvent += ResetDecreasePlayerHype;
            _hypeEnemy.DecreaseHypeEvent += ResetDecreasePlayerHype;
        }

        private void ResetDecreasePlayerHype(float amount) => _isDecreasePlayerHype = false;

        public void DisabledService()
        {
            UnityEngine.AddressableAssets.Addressables.Release(_hypeServiceSo);
            _hypeEnemy.DecreaseHypeEvent = null;
            _hypeEnemy.IncreaseHypeEvent = null;
            _hypeEnemy.GainUltimateEvent = null;
            _hypeEnemy.LoseUltimateEvent = null;
            _hypeEnemy.SetHypeEvent = null;
            _hypePlayer.DecreaseHypeEvent = null;
            _hypePlayer.IncreaseHypeEvent = null;
            _hypePlayer.GainUltimateEvent = null;
            _hypePlayer.LoseUltimateEvent = null;
            _hypePlayer.SetHypeEvent = null;
            ReachMaximumHypeEvent = null;
            ReachMinimumHypeEvent = null;
            UpdateManager.UnRegister(this);
            RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        }

        public bool GetIsActiveService { get; }

        public void SetRemoteConfigurableValues()
        {
            _hypeServiceSo.MaxHype = RemoteConfigManager.Config.GetFloat("MaxHype");
            _hypeServiceSo.AmountPlayerHypeDecrease = RemoteConfigManager.Config.GetFloat("AmountPlayerHypeDecrease");
            _hypeServiceSo.EnemyHypeSO.StartValue = RemoteConfigManager.Config.GetFloat("EnemyHypeStartValue");
            _hypeServiceSo.EnemyHypeSO.UltimateValue = RemoteConfigManager.Config.GetFloat("EnemyHypeUltimateValue");
            _hypeServiceSo.PlayerHypeSO.StartValue = RemoteConfigManager.Config.GetFloat("PlayerHypeStartValue");
            _hypeServiceSo.PlayerHypeSO.UltimateValue = RemoteConfigManager.Config.GetFloat("PlayerHypeUltimateValue");
            _hypeServiceSo.TimeBeforePlayerHypeDecrease =
                RemoteConfigManager.Config.GetFloat("TimeBeforePlayerHypeDecrease");
        }

        public void OnUpdate()
        {
            if (_isDecreasePlayerHype)
            {
                DecreaseHypeInUpdate();
            }
            else
            {
                _decreasePlayerHypeTimer += Time.deltaTime;
                if (_decreasePlayerHypeTimer > _hypeServiceSo.TimeBeforePlayerHypeDecrease)
                {
                    _decreasePlayerHypeTimer = 0;
                    _isDecreasePlayerHype = true;
                }

            }
        }
    }
}