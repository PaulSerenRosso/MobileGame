using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Action
{
    public class RotationAction : MonoBehaviour, IAction, IUpdatable
    {
        public Transform LookTarget;

        public void OnUpdate()
        {
            Vector3 newForward = LookTarget.position - transform.position;
            newForward.y = transform.position.y;
            transform.forward = newForward.normalized;
        }

        public void MakeAction()
        {
            UpdateManager.Register(this);
        }
    }   
}
