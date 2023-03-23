using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Handler
{
    public abstract class PlayerHandlerRecordable : PlayerHandler
    {
     [SerializeField] private PlayerAction[] allActionsWhichRecord;
      [SerializeField] private PlayerAction[] allActionsBlockedLaunchRecordAction;
        [SerializeField] protected PlayerHandlerRecorderManager _playerHandlerRecordableManager;

        public override void Setup(params object[] arguments)
        {
            for (int i = 0; i < allActionsBlockedLaunchRecordAction.Length; i++)
            {
                allActionsBlockedLaunchRecordAction[i].EndActionEvent += CheckActionsBlockedRecord;
            }
        }
        protected override void TryMakeAction(params object[] args)
        {
            RecordInput(args);
            base.TryMakeAction(args);
        }

        private void RecordInput(object[] args)
        {
            for (int i = 0; i < allActionsWhichRecord.Length; i++)
            {
                Debug.Log(allActionsWhichRecord[i].IsInAction +"   " +allActionsWhichRecord[i]);
                if (allActionsWhichRecord[i].IsInAction)
                {
                    Debug.Log("record");
                    _playerHandlerRecordableManager.argsForInputPlayerActionRecorded = args;
                    _playerHandlerRecordableManager.InputPlayerActionRecorded = TryMakeAction;
                    return;
                }
            }
        }

        public virtual void CheckActionsBlockedRecord()
        {
      
            if (_playerHandlerRecordableManager.InputPlayerActionRecorded != null)
            {
                for (int i = 0; i < allActionsBlockedLaunchRecordAction.Length; i++)
                {
                    if (allActionsBlockedLaunchRecordAction[i].IsInAction)
                    {
                        return;
                    }
                }
                _playerHandlerRecordableManager.LaunchRecorderAction();
            }
        }
    }
}