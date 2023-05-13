using System;

namespace Service.Fight
{
    public interface IFightService : IService
    {
        public void StartFight(string environmentAddressableName, string enemyAddressableName, bool isDebugFight, bool isTutorialFight);
        public void QuitFight();
        public bool GetFightTutorial();

        public void ActivatePause(Action callback);
        public void DeactivatePause();

        public void ActivatePausePlayer();
        public void DeactivatePausePlayer();

        public event Action<int> InitiateRoundEvent;
        public event Action EndInitiateRoundEvent;
        
        public event Action ActivatePauseEvent;
        public event Action DeactivatePauseEvent;

        public event Action<bool> EndFightEvent;
    }
}