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
        
        public void Init()
        {
            _playerMovementHandler.MakeActionEvent += SetDirParamater;
            _movementAction.MakeUpdateEvent += RecoveryMovement;
        }
    }
}