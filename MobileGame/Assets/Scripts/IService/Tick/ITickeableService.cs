using System.Collections;
using System.Collections.Generic;
using Service;
using UnityEngine;

public interface ITickeableService : IService
{
  void Tick();
}
