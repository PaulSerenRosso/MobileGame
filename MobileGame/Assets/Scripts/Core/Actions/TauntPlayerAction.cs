using HelperPSR.Tick;
using Service.Hype;
using TMPro;
using UnityEngine;

namespace Actions
{
    public class TauntPlayerAction :  PlayerAction
    {
        public TauntActionSO SO;

        [SerializeField] private TextMeshPro _tauntText;

        private TickTimer _endTauntTimer;
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
            Debug.Log("je taunt whouah");
            _isTaunting = true;
            _tauntText.text = "Start Taunt";
            _startTauntTimer.Initiate();
            _isStartTaunting = true;
        }

        private void Taunt()
        {
            _isStartTaunting = false;
            MakeActionEvent?.Invoke();
            _tauntText.text = "Taunt";
        }

        public override void SetupAction(params object[] arguments)
        {
            _startTauntTimer = new TickTimer(SO.StartTime, (TickManager)arguments[0]);
            
            _endTauntTimer = new TickTimer(SO.EndTime, (TickManager)arguments[0]);
            _endTauntTimer.TickEvent += TickEndTaunt;
            _tauntText.text = "";
            _startTauntTimer.TickEvent += Taunt;
            _hypeService =(IHypeService) arguments[1];
        }
        

        public event System.Action CancelActionEvent;
        public void TryCancelTaunt()
        {
            if (_isTaunting)
            {
                if (_isStartTaunting)
                {
                    _startTauntTimer.TickEvent += TickCancelTaunt;
                }
                else
                {
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
            _endTauntTimer.Initiate();
            _tauntText.text = "Cancel";
            CancelActionEvent?.Invoke();
        }

        void TickEndTaunt()
        {
            _isTaunting = false;
            _tauntText.text = "";
            EndActionEvent?.Invoke();
            
        }
    }
}