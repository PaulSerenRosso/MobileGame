using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;

namespace Player.Handler
{
    public abstract class PlayerHandlerRecordable : PlayerHandler
    {
        [SerializeField] private PlayerAction[] allActionsBlockedRecord;
        [SerializeField] private PlayerHandlerRecorderManager _playerHandlerRecordableManager;

        public override void Setup(params object[] arguments)
        {
            for (int i = 0; i < allActionsBlockedRecord.Length; i++)
            {
                allActionsBlockedRecord[i].EndActionEvent += CheckActionsBlockedRecord;
            }
        }
        protected override void TryMakeAction(params object[] args)
        {
            RecordInput(args);
            base.TryMakeAction(args);
        }

        private void RecordInput(object[] args)
        {
            for (int i = 0; i < allActionsBlockedRecord.Length; i++)
            {
                if (allActionsBlockedRecord[i].IsInAction)
                {
                    _playerHandlerRecordableManager.argsForInputPlayerActionRecorded = args;
                    _playerHandlerRecordableManager.InputPlayerActionRecorded = TryMakeAction;
                    return;
                }
            }
        }

        public void CheckActionsBlockedRecord()
        {
            if (_playerHandlerRecordableManager.InputPlayerActionRecorded != null)
            {
                for (int i = 0; i < allActionsBlockedRecord.Length; i++)
                {
                    if (allActionsBlockedRecord[i].IsInAction)
                    {
                        return;
                    }
                }
                Debug.Log("bonsoir");
                _playerHandlerRecordableManager.InputPlayerActionRecorded.Invoke(_playerHandlerRecordableManager.argsForInputPlayerActionRecorded);
                _playerHandlerRecordableManager.InputPlayerActionRecorded = null;

            }
        }
    }
}