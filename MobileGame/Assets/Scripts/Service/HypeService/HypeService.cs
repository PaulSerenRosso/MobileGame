using System;
using Addressables;
using HelperPSR.RemoteConfigs;

namespace Service.Hype
{
    public class HypeService : IHypeService, IRemoteConfigurable
    {
        private HypeServiceSO _hypeServiceSo;
        private float _currentHype;

        public void IncreaseHype(float amount)
        {
            if (CheckMaximumHypeIsReached(_currentHype)) return;
            if (CheckMaximumHypeIsReached(_currentHype + amount))
            {
                _currentHype = _hypeServiceSo.MaxHype;
                ReachMaximumHypeEvent?.Invoke(amount);
            }
            else
            {
                _currentHype += amount;
            }

            IncreaseHypeEvent?.Invoke(amount);
        }

        public void DecreaseHype(float amount)
        {
            if (CheckMinimumHypeIsReached(_currentHype)) return;
            if (CheckMinimumHypeIsReached(_currentHype - amount))
            {
                _currentHype = _hypeServiceSo.MinHype;
                ReachMinimumHypeEvent?.Invoke(amount);
            }
            else
            {
                _currentHype -= amount;
            }

            IncreaseHypeEvent?.Invoke(amount);
        }

        public void SetHype(float value)
        {
            if (!CheckHypeIsClamped(value)) return;
            _currentHype = value;
            SetHypeEvent?.Invoke();
        }

        public float GetCurrentHype()
        {
            return _currentHype;
        }

        public float GetMaximumHype()
        {
            return _hypeServiceSo.MaxHype;
        }

        public float GetMinimumHype()
        {
            return _hypeServiceSo.MinHype;
        }

        private bool CheckHypeIsClamped(float value)
        {
            return value <= _hypeServiceSo.MaxHype && value >= _hypeServiceSo.MinHype;
        }

        private bool CheckMaximumHypeIsReached(float currentHype)
        {
            return currentHype >= _hypeServiceSo.MaxHype;
        }

        private bool CheckMinimumHypeIsReached(float currentHype)
        {
            return currentHype <= _hypeServiceSo.MinHype;
        }

        public event Action<float> IncreaseHypeEvent;
        public event Action<float> DecreaseHypeEvent;
        public event Action<float> ReachMaximumHypeEvent;
        public event Action<float> ReachMinimumHypeEvent;
        public event Action SetHypeEvent;

        public void EnabledService()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<HypeServiceSO>("HypeSO", SetHypeSO);
        }

        private void SetHypeSO(HypeServiceSO hypeServiceSo)
        {
            _hypeServiceSo = hypeServiceSo;
            RemoteConfigManager.RegisterRemoteConfigurable(this);
            SetHype(_hypeServiceSo.BaseValueHype);
        }

        public void DisabledService()
        {
            UnityEngine.AddressableAssets.Addressables.Release(_hypeServiceSo);
            DecreaseHypeEvent = null;
            IncreaseHypeEvent = null;
            ReachMaximumHypeEvent = null;
            ReachMinimumHypeEvent = null;
            RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        }

        public bool GetIsActiveService { get; }

        public void SetRemoteConfigurableValues()
        {
            _hypeServiceSo.MinHype = RemoteConfigManager.Config.GetFloat("MinHype");
            _hypeServiceSo.MaxHype = RemoteConfigManager.Config.GetFloat("MaxHype");
            _hypeServiceSo.BaseValueHype = RemoteConfigManager.Config.GetFloat("BaseValueHype");
        }
    }
}