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

    public void LaunchRecorderAction()
    {
        Debug.Log("trylaunchrecorderaction");
        if(InputPlayerActionRecorded == null) return;
        Debug.Log("launchrecorderaction");
       InputPlayerActionRecorded.Invoke(argsForInputPlayerActionRecorded);
        InputPlayerActionRecorded = null;
        argsForInputPlayerActionRecorded = null;
    }

}
    
}
