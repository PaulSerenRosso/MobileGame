using UnityEngine;

namespace Actions
{
    public abstract class PlayerAction : MonoBehaviour
    {
        public abstract bool IsInAction { get; }

        public abstract void MakeAction();
        public abstract void SetupAction(params object[] arguments);

        public virtual void UnlinkAction()
        {
            MakeActionEvent = null;
            EndActionEvent = null;
        }

        public System.Action MakeActionEvent;
        public System.Action EndActionEvent;
    }
}