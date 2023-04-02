using System;
using Attributes;
using HelperPSR.Tick;

namespace Service
{
    public class TickService : ITickeableService
    {
        public event Action tickEvent;

        private TickManager _tickManager;
        private float _tickRate = 10f;

        public TickManager GetTickManager { get => _tickManager; }

        [ServiceInit]
        private void Setup()
        {
            _tickManager = new TickManager(_tickRate);
        }
    }
}