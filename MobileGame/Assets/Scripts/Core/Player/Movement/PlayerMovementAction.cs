using HelperPSR.MonoLoopFunctions;
using Service.Inputs;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementAction : MonoBehaviour, IPlayerAction, IUpdatable
    {
        public Swipe CurrentSwipe;
        public MovePoint CurrentMovePoint;
        public PlayerMovementSO PlayerMovementSO;

        private Vector3 _startPosition;
        private float _timer;
        private bool _isMoving;
        private float _ratioTime;

        public void OnUpdate()
        {
            if (_timer >= PlayerMovementSO.MaxTime)
            {
                _isMoving = false;
                UpdateManager.UnRegister(this);
                transform.position = CurrentMovePoint.Position;
                return;
            }
            _timer += Time.deltaTime;
            _ratioTime = _timer / PlayerMovementSO.MaxTime;
            transform.position = Vector3.Lerp(_startPosition, CurrentMovePoint.Position, PlayerMovementSO.CurvePosition.Evaluate(_ratioTime));
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        public void MakeAction()
        {
            _startPosition = transform.position;
            UpdateManager.Register(this);
            _isMoving = true;
            _timer = 0;
        }

        public void Move()
        {
            transform.position = CurrentMovePoint.Position;
        }
    }
}