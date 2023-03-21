using HelperPSR.Tick;
using TMPro;
using UnityEngine;

namespace Action
{
    public class TauntAction : MonoBehaviour, IAction
    {
        public TauntActionSO SO;

        [SerializeField] private TextMeshPro _tauntText;

        private TickTimer _endTauntTimer;
        private bool _isTaunting;

        public bool IsInAction
        {
            get => _isTaunting;
        }


        public void MakeAction()
        {
            Debug.Log("je taunt whouah");
            _isTaunting = true;
            MakeActionEvent?.Invoke();
            _tauntText.text = "Taunt";
        }

        public void SetupAction(params object[] arguments)
        {
            _endTauntTimer = new TickTimer(SO.EndTime, (TickManager)arguments[0]);
            _endTauntTimer.TickEvent += TickEndTaunt;
            _tauntText.text = "";
        }

        public event System.Action MakeActionEvent;

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
            
        }
    }
}