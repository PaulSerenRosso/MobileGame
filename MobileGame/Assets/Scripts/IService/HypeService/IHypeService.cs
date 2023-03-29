using System;

namespace Service.Hype
{
    public interface IHypeService : ISwitchableService
    {
        void IncreaseHype(float amount);
        void DecreaseHype(float amount);
        void SetHype(float value);

        float GetCurrentHype();
        float GetMaximumHype();
        float GetMinimumHype();
        event Action<float> IncreaseHypeEvent;
        event Action<float> DecreaseHypeEvent;
        event Action<float> ReachMaximumHypeEvent;
        event Action<float> ReachMinimumHypeEvent;
        event Action SetHypeEvent;
    }
}