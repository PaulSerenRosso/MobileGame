using HelperPSR.MonoLoopFunctions;
using HelperPSR.Tick;
using Service.Hype;
using UnityEngine;

namespace Actions
{
    public class TauntPlayerAction : PlayerAction, IUpdatable
    {
        public TauntActionSO SO;
        
        private TickTimer _startTauntTimer;
        private bool _isTaunting;
        private bool _isStartTaunting;
        private IHypeService _hypeService;

        public override bool IsInAction
        {
            get => _isTaunting;
        }

        public override void MakeAction()
        {
            if (!_isTaunting)
            {
                _isTaunting = true;
                _startTauntTimer.TickEvent += Taunt;
                _startTauntTimer.Initiate();
                _isStartTaunting = true;
            }
        }

        private void Taunt()
        {
            _isStartTaunting = false;
            MakeActionEvent?.Invoke();
            UpdateManager.Register(this);
        }

        public override void SetupAction(params object[] arguments)
        {
            _startTauntTimer = new TickTimer(SO.StartTime, (TickManager)arguments[0]);
            _hypeService = (IHypeService)arguments[1];
        }
        
        public void TryCancelTaunt()
        {
            if (_isTaunting)
            {
           
                if (_isStartTaunting)
                {
                    _startTauntTimer.TickEvent -= Taunt;
                    _startTauntTimer.TickEvent += TickCancelTaunt;
                }
                else
                {
                    _startTauntTimer.TickEvent -= Taunt;
                    CancelTaunt();
                }
            }
        }

        private void TickCancelTaunt()
        {
            CancelTaunt();
            _startTauntTimer.TickEvent -= TickCancelTaunt;
        }

        private void CancelTaunt()
        {
            _isTaunting = false;
            UpdateManager.UnRegister(this);
            EndActionEvent?.Invoke();
        }
        
        public void OnUpdate()
        {
            _hypeService.IncreaseHypePlayer(SO.HypeAmount * Time.deltaTime);
        }

        private void OnDisable()
        {
            UpdateManager.UnRegister(this);
        }

        public override void UnlinkAction()
        {
            base.UnlinkAction();
            UpdateManager.UnRegister(this);
            _startTauntTimer.ResetEvents();
            _startTauntTimer.Cancel();
        }
    }
}