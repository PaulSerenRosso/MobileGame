using System.Numerics;
using Action;
using Player.Handler;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementAction _movementAction;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private TauntAction _tauntAction; 
        public void Init()
        {
            /*
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
            _movementAction.MakeActionEvent += ResetEndMovementAnimationParameter;
            _tauntAction.MakeActionEvent += ActivateTauntFX;
            _tauntAction.CancelActionEvent += DeactivateTauntFX;
            */
            
        }
    }
}