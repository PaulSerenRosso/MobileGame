using HelperPSR.Tick;
using TMPro;
using UnityEngine;

namespace Action
{
    public class TauntAction : MonoBehaviour, IAction
    {
        [SerializeField] private TauntActionSO so;

        private TickTimer endTauntTimer;
        [SerializeField] private TextMeshPro _tauntText;

        public bool IsInAction
        {
            get => isTaunting;
        }

        private bool isTaunting;


        public void MakeAction()
        {
            Debug.Log("je taunt whouah");
            isTaunting = true;
            MakeActionEvent?.Invoke();
            _tauntText.text = "Taunt";
        }

        public void SetupAction(params object[] arguments)
        {
            endTauntTimer = new TickTimer(so.endTime, (TickManager)arguments[0]);
            endTauntTimer.TickEvent += TickEndTaunt;
            _tauntText.text = "";
        }

        public event System.Action MakeActionEvent;

        public void CancelTaunt()
        {
            if (isTaunting)
            {
                endTauntTimer.Initiate();
                _tauntText.text = "Cancel";
            }
        }

        void TickEndTaunt()
        {
            isTaunting = false;
            _tauntText.text = "";
        }
    }
}