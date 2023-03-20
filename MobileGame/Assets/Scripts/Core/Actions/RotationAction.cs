using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Action
{
    public class RotationAction : MonoBehaviour, IAction, IUpdatable
    {
        private Transform _lookTarget;

        public void OnUpdate()
        {
            Vector3 newForward = _lookTarget.position - transform.position;
            newForward.y = transform.position.y;
            transform.forward = newForward.normalized;
        }

        public bool IsInAction { get; }

        public void MakeAction()
        {
            UpdateManager.Register(this);
        }

        public void SetupAction(params object[] arguments)
        {
            _lookTarget = (Transform)arguments[0];
        }

        public event System.Action MakeActionEvent;
    }
}