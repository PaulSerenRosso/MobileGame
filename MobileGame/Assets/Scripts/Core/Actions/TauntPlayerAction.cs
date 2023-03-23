using HelperPSR.Tick;
using TMPro;
using UnityEngine;

namespace Actions
{
    public class TauntPlayerAction :  PlayerAction
    {
        public TauntActionSO SO;

        [SerializeField] private TextMeshPro _tauntText;

        private TickTimer _endTauntTimer;
        private bool _isTaunting;

        public override bool IsInAction
        {
            get => _isTaunting;
        }


        public override void MakeAction()
        {
            Debug.Log("je taunt whouah");
            _isTaunting = true;
            MakeActionEvent?.Invoke();
            _tauntText.text = "Taunt";
        }

        public override void SetupAction(params object[] arguments)
        {
            _endTauntTimer = new TickTimer(SO.EndTime, (TickManager)arguments[0]);
            _endTauntTimer.TickEvent += TickEndTaunt;
            _tauntText.text = "";
        }
        

        public event System.Action CancelActionEvent;
        public void CancelTaunt()
        {
            if (_isTaunting)
            {
                _endTauntTimer.Initiate();
                _tauntText.text = "Cancel";
                CancelActionEvent?.Invoke();
            }
        }

        void TickEndTaunt()
        {
            _isTaunting = false;
            _tauntText.text = "";
            EndActionEvent?.Invoke();
            
        }
    }
}