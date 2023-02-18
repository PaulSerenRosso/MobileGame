using System;
using HelperPSR.Tick;
using Service;

public interface ITickeableService : IService
{
    TickManager GetTickManager { get;  }
    event Action tickEvent;
}