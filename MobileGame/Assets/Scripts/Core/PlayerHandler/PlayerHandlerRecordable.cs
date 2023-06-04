using Actions;
using UnityEngine;

namespace Player.Handler
{
    public abstract class PlayerHandlerRecordable : PlayerHandler
    {
        [SerializeField] private PlayerAction[] _allActionsWhichRecord;
        [SerializeField] private PlayerAction[] _allActionsBlockedLaunchRecordAction;
        [SerializeField] protected PlayerHandlerRecorderManager _playerHandlerRecordableManager;

        public override void Setup(params object[] arguments)
        {
            for (int i = 0; i < _allActionsBlockedLaunchRecordAction.Length; i++)
            {
                _allActionsBlockedLaunchRecordAction[i].EndActionEvent += CheckActionsBlockedRecord;
            }
        }

        protected override void TryMakeAction(params object[] args)
        {
            TryRecordInput(args);
            base.TryMakeAction(args);
        }

        protected virtual bool TryRecordInput(object[] args)
        {
            for (int i = 0; i < _allActionsWhichRecord.Length; i++)
            {
                if (_allActionsWhichRecord[i].IsInAction)
                {
                    SendRecordAction(args);
                    return true;
                }
            }

            return false;
        }

        protected void SendRecordAction(object[] args)
        {
            _playerHandlerRecordableManager.argsForInputPlayerActionRecorded = args;
            _playerHandlerRecordableManager.InputPlayerActionRecorded = TryMakeAction;
        }

        public void CheckActionsBlockedRecord()
        {
            if (_playerHandlerRecordableManager.InputPlayerActionRecorded == TryMakeAction)
            {
                if (CheckBlockedActionsIsRunning()) return;
                if(CheckActionsBlockedCustomCondition())
                    _playerHandlerRecordableManager.LaunchRecorderAction();
            }
        }

        protected virtual bool CheckActionsBlockedCustomCondition()
        {
            return true;
        }

        private bool CheckBlockedActionsIsRunning()
        {
            for (int i = 0; i < _allActionsBlockedLaunchRecordAction.Length; i++)
            {
                if (_allActionsBlockedLaunchRecordAction[i].IsInAction)
                {
                    return true;
                }
            }

            return false;
        }
        
        public void CancelRecord()
        {
            _playerHandlerRecordableManager.InputPlayerActionRecorded = null;
            _playerHandlerRecordableManager.argsForInputPlayerActionRecorded = null; 
        }
    }
}