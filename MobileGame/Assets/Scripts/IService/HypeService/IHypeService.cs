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
        void ResetHypePlayer();
        void ResetHypeEnemy();
        void ActivateDecreaseUpdateHypePlayer();
        void DeactivateDecreaseUpdateHypePlayer();

        float GetCurrentHypePlayer();
        float GetCurrentHypeEnemy();
        float GetMaximumHype();
        bool GetUltimateAreaPlayer();
        bool GetUltimateAreaEnemy();

        Action<float> GetPlayerIncreaseHypeEvent{ set; get; }
        Action<float> GetPlayerDecreaseHypeEvent{ set;get; }
        Action<float> GetPlayerSetHypeEvent { set;get; }
        Action<float> GetPlayerGainUltimateEvent { set; get;}
        Action<float> GetPlayerLoseUltimateEvent { set;get; }
      
        Action<float> GetEnemyIncreaseHypeEvent{ set;get; }
        Action<float> GetEnemyDecreaseHypeEvent{ set; get;}
        Action<float> GetEnemySetHypeEvent { get; set; }
        Action<float> GetEnemyGainUltimateEvent{ set;get; }
        Action<float> GetEnemyLoseUltimateEvent{ set; get;}

        event Action<float> ReachMaximumHypeEvent;
        event Action<float> ReachMinimumHypeEvent;
    }
}