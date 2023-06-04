using Actions;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerRotationHandler : PlayerHandler
    {
        [SerializeField] private RotationPlayerAction rotationPlayerAction;
        
        protected override Actions.PlayerAction GetAction()
        {
            return rotationPlayerAction;
        }

        public override void InitializeAction() { }

        public override void Setup(params object[] arguments)
        {
            rotationPlayerAction.SetupAction((Transform)arguments[0]);
            rotationPlayerAction.MakeAction();
        }
    }
}