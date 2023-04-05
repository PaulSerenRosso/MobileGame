using System;

namespace Service.Hype
{
    public interface IHypeService : ISwitchableService
    {
        void IncreaseHypePlayer(float amount);
        void IncreaseHypeEnemy(float amount);
        void DecreaseHypePlayer(float amount);
        void DecreaseHypeEnemy(float amount);
        void SetHypePlayer(float value);
        void SetHypeEnemy(float value);

        float GetCurrentHypePlayer();
        float GetCurrentHypeEnemy();
        float GetMaximumHype();
        float GetMinimumHype();
        event Action<float> IncreaseHypePlayerEvent;
        event Action<float> IncreaseHypeEnemyEvent;
        event Action<float> DecreaseHypePlayerEvent;
        event Action<float> DecreaseHypeEnemyEvent;

        event Action<float> ReachMaximumHypeEvent;
        event Action<float> ReachMinimumHypeEvent;
        event Action SetHypePlayerEvent;
        event Action SetHypeEnemyEvent;

    }
}