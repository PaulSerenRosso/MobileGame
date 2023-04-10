using System;

namespace Service.Fight
{
    public interface IFightService : IService
    {
        public void StartFight(string environmentAddressableName);
        
        public event Action<int> InitiateRoundEvent;
        public event Action EndInitiateRoundEvent;
        
        public event Action ActivatePauseEvent;
        public event Action DeactivatePauseEvent;
    }
}