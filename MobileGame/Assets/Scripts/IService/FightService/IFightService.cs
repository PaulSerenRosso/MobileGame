using System;

namespace Service.Fight
{
    public interface IFightService : IService
    {
        public void StartFight(string environmentAddressableName, string enemyAddressableName, bool isDebugFight, bool isTutorialFight);
        public void QuitFight();
        void QuitFightTutorial(bool value);
        public bool GetFightTutorial();
        public bool GetFightDebug();
        public EnemyGlobalSO GetEnemySO();

        public void ActivatePause(Action callback);
        public void DeactivatePause();

        public void ActivatePausePlayer();
        public void DeactivatePausePlayer();

        public void StopCinematic();

        public event Action<int> InitiateRoundEvent;
        public event Action EndInitiateRoundEvent;
        
        public event Action ActivatePauseEvent;
        public event Action DeactivatePauseEvent;

        public event Action<bool> EndFightEvent;

        public event Action ActivateFightCinematic;
        public event Action DeactivateFightCinematic;
    }
}