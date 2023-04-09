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
            RecordInput(args);
            base.TryMakeAction(args);
        }

        private void RecordInput(object[] args)
        {
            for (int i = 0; i < _allActionsWhichRecord.Length; i++)
            {
                if (_allActionsWhichRecord[i].IsInAction)
                {
                    _playerHandlerRecordableManager.argsForInputPlayerActionRecorded = args;
                    _playerHandlerRecordableManager.InputPlayerActionRecorded = TryMakeAction;
                    return;
                }
            }
        }

        public void CheckActionsBlockedRecord()
        {
            if (_playerHandlerRecordableManager.InputPlayerActionRecorded == TryMakeAction)
            {
                if (CheckBlockedActionsIsRunning()) return;
                _playerHandlerRecordableManager.LaunchRecorderAction();
            }
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
    }
}