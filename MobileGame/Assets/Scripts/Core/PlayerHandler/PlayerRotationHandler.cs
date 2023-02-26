using Action;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerRotationHandler : PlayerHandler<RotationAction>
    {
        public override void InitializeAction()
        {
            
        }

        public override void Setup(params object[] arguments)
        {
            _action.SetupAction((Transform)arguments[0]);
            _action.MakeAction();
        }
    }
}