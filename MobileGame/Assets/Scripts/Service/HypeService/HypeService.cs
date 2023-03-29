using System;
using Addressables;

namespace Service.Hype
{
    public class HypeService : IHypeService
    {
        private HypeServiceSO _hypeServiceSo;
        private float _currentHype;

        public void IncreaseHype(float amount)
        {
            if (!CheckMaximumHypeIsReached(_currentHype))
            {
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
        }

        public void DecreaseHype(float amount)
        {
            if (!CheckMinimumHypeIsReached(_currentHype))
            {
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
        }

        public void SetHype(float value)
        {
            if (CheckHypeIsClamped(value))
            {
                _currentHype = value;
                SetHypeEvent?.Invoke();
            }
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
            if (value <= _hypeServiceSo.MaxHype && value >= _hypeServiceSo.MinHype)
                return true;
            return false;
        }

        private bool CheckMaximumHypeIsReached(float currentHype)
        {
            if (currentHype >= _hypeServiceSo.MaxHype)
            {
                return true;
            }

            return false;
        }

        private bool CheckMinimumHypeIsReached(float currentHype)
        {
            if (currentHype <= _hypeServiceSo.MinHype)
            {
                return true;
            }

            return false;
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
            SetHype(_hypeServiceSo.BaseValueHype);
        }

        public void DisabledService()
        {
            UnityEngine.AddressableAssets.Addressables.Release(_hypeServiceSo);
            DecreaseHypeEvent = null;
            IncreaseHypeEvent = null;
            ReachMaximumHypeEvent = null;
            ReachMinimumHypeEvent = null;
        }

        public bool GetIsActiveService { get; }
    }
}