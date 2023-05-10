using System;

namespace Service.Fight
{
    public interface IFightService : IService
    {
        public void StartFight(string environmentAddressableName, string enemyAddressableName, bool isDebugFight);
        public void QuitFight();

        public event Action<int> InitiateRoundEvent;
        public event Action EndInitiateRoundEvent;
        
        public event Action ActivatePauseEvent;
        public event Action DeactivatePauseEvent;

        public event Action<bool> EndFightEvent;
    }
}