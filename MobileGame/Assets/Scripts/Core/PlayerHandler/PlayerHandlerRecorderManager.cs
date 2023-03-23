using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Handler
{
public class PlayerHandlerRecorderManager : MonoBehaviour
{
    public object[] argsForInputPlayerActionRecorded;
    public Action<object[]> InputPlayerActionRecorded;
}
    
}
