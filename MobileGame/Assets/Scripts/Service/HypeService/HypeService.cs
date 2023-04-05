using System;
using Addressables;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using UnityEngine;

namespace Service.Hype
{
    public class HypeService : IHypeService, IRemoteConfigurable, IUpdatable
    {
        private HypeServiceSO _hypeServiceSo;
        private HypeSO _playerHypeSO;
        private HypeSO _enemyHypeSO;
        private Hype _hypePlayer;
        private Hype _hypeEnemy;

        private bool _inCooldown;

        public void IncreaseHypePlayer(float amount)
        {
            IncreaseHype(amount, _hypePlayer, IncreaseHypePlayer, _hypeEnemy);
        }

        public void IncreaseHypeEnemy(float amount)
        {
            IncreaseHype(amount, _hypeEnemy, IncreaseHypeEnemy, _hypePlayer);
        }

        public void IncreaseHype(float amount, Hype hype, Action<float> callback, Hype otherHype)
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
                    hype.CurrentValue = _hypeServiceSo.MaxHype - otherHype.CurrentValue;
                }
                else
                {
                    hype.CurrentValue += amount;
                }
            }

            callback?.Invoke(amount);
        }

        public void DecreaseHypePlayer(float amount)
        {
            DecreaseHype(amount, _hypePlayer, DecreaseHypePlayer);
        }

        public void DecreaseHypeEnemy(float amount)
        {
            DecreaseHype(amount, _hypeEnemy, DecreaseHypeEnemy);
        }

        public void DecreaseHype(float amount, Hype hype, Action<float> callback)
        {
            if (CheckMinimumHypeIsReached(hype.CurrentValue)) return;
            if (CheckMinimumHypeIsReached(hype.CurrentValue - amount))
            {
                hype.CurrentValue = 0;
                ReachMinimumHypeEvent?.Invoke(amount);
            }
            else
            {
                hype.CurrentValue -= amount;
            }

            callback?.Invoke(amount);
        }

        public void SetHypePlayer(float value)
        {
            if (!CheckHypeIsClamped(value)) return;
            _hypePlayer.CurrentValue = value;
            SetHypePlayerEvent?.Invoke();
        }

        public void SetHypeEnemy(float value)
        {
            if (!CheckHypeIsClamped(value)) return;
            _hypeEnemy.CurrentValue = value;
            SetHypeEnemyEvent?.Invoke();
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
            return !(hype.CurrentValue + amount + otherHype.CurrentValue >= _hypeServiceSo.MaxHype);
        }

        private bool CheckMaximumHypeIsReached(float currentHype)
        {
            return currentHype >= _hypeServiceSo.MaxHype;
        }

        private bool CheckMinimumHypeIsReached(float currentHype)
        {
            return currentHype <= 0;
        }

        public event Action<float> IncreaseHypePlayerEvent;
        public event Action<float> IncreaseHypeEnemyEvent;
        public event Action<float> DecreaseHypePlayerEvent;
        public event Action<float> DecreaseHypeEnemyEvent;
        public event Action<float> ReachMaximumHypeEvent;
        public event Action<float> ReachMinimumHypeEvent;
        public event Action SetHypePlayerEvent;
        public event Action SetHypeEnemyEvent;
        
        public void EnabledService()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<HypeServiceSO>("HypeSO", SetHypeSO);
        }

        private void DecreaseHypeInUpdate()
        {
            DecreaseHype(_hypeServiceSo.AmountHypeDecreaseTime * Time.deltaTime, _hypePlayer, DecreaseHypePlayer);
        }

        private void SetHypeSO(HypeServiceSO hypeServiceSo)
        {
            _hypeServiceSo = hypeServiceSo;
            RemoteConfigManager.RegisterRemoteConfigurable(this);
            SetHypePlayer(_hypeServiceSo.PlayerHypeSO.StartValue);
            SetHypeEnemy(_hypeServiceSo.EnemyHypeSO.StartValue);
            UpdateManager.Register(this);
        }

        public void DisabledService()
        {
            UnityEngine.AddressableAssets.Addressables.Release(_hypeServiceSo);
            DecreaseHypePlayerEvent = null;
            DecreaseHypeEnemyEvent = null;
            IncreaseHypePlayerEvent = null;
            IncreaseHypeEnemyEvent = null;
            ReachMaximumHypeEvent = null;
            ReachMinimumHypeEvent = null;
            UpdateManager.UnRegister(this);
            RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        }

        public bool GetIsActiveService { get; }

        public void SetRemoteConfigurableValues()
        {
            _hypeServiceSo.MaxHype = RemoteConfigManager.Config.GetFloat("MaxHype");
            _hypeServiceSo.AmountHypeDecreaseTime = RemoteConfigManager.Config.GetFloat("AmountHypeDecreaseTime");
        }

        public void OnUpdate()
        {
            DecreaseHypeInUpdate();
        }
    }
}