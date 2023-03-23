using System;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.Tick;
using UnityEngine;

namespace Actions
{
    public class MovementPlayerAction : PlayerAction, IFixedUpdate
    {
        public Vector3 Destination;

        [SerializeField] private MovementSO _movementSO;
        [SerializeField] private Rigidbody _rb;

      
        private Vector3 _startPosition;
        private float _timer;
        private bool _isMoving;
        private float _ratioTime;

        public event Action ReachDestinationEvent;
        public float GetMaxTimeMovement()
        {
            return _movementSO.MaxTime;
        }
        public void OnFixedUpdate()
        {
            if (_timer >= _movementSO.MaxTime)
            {
                _isMoving = false;
                transform.position = Destination;
                FixedUpdateManager.UnRegister(this);
                ReachDestinationEvent?.Invoke();
                EndActionEvent?.Invoke();
                return;
            }
            _timer += Time.fixedDeltaTime;
            _ratioTime = _timer / _movementSO.MaxTime;
            MakeUpdateEvent?.Invoke(_ratioTime);
            _rb.position = Vector3.Lerp(_startPosition, Destination, _movementSO.CurvePosition.Evaluate(_ratioTime));
        }

        
        public override bool IsInAction => _isMoving;

        public override  void MakeAction()
        {
            _startPosition = _rb.position;
            _isMoving = true;
            _timer = 0;
            FixedUpdateManager.Register(this);
            MakeActionEvent?.Invoke();
        }

        public override  void SetupAction(params object[] arguments)
        {
            Destination = (Vector3)arguments[0];
            MoveToDestination();
        }

        public event System.Action MakeActionEvent;
        public event System.Action<float> MakeUpdateEvent;

        public void MoveToDestination()
        {
            transform.position = Destination;
        }
    }
}