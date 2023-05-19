using System;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerHandlerRecorderManager : MonoBehaviour
    {
        public object[] argsForInputPlayerActionRecorded;
        public Action<object[]> InputPlayerActionRecorded;

        public void LaunchRecorderAction()
        {
            if (InputPlayerActionRecorded == null) return;
            InputPlayerActionRecorded.Invoke(argsForInputPlayerActionRecorded);
            InputPlayerActionRecorded = null;
            argsForInputPlayerActionRecorded = null;
        }
    }
}