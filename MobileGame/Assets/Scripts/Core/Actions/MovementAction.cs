using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using UnityEngine;

namespace Action
{
    public class MovementAction : MonoBehaviour, IAction, IFixedUpdate
    {
        public Vector3 Destination;
        [SerializeField] private MovementSO _movementSO;

        private Vector3 _startPosition;
        private float _timer;
        private bool _isMoving;
        private float _ratioTime;
        [SerializeField] private Rigidbody rb;

        public void OnFixedUpdate()
        {
            if (_timer >= _movementSO.MaxTime)
            {
                _isMoving = false;
                transform.position = Destination;
                FixedUpdateManager.UnRegister(this);
                return;
            }
            _timer += Time.fixedDeltaTime;
            _ratioTime = _timer / _movementSO.MaxTime;
            rb.position = Vector3.Lerp(_startPosition, Destination, _movementSO.CurvePosition.Evaluate(_ratioTime));
        }

        public bool IsInAction { get => _isMoving; }

        public void MakeAction()
        {
            _startPosition = rb.position;
            _isMoving = true;
            _timer = 0;
            FixedUpdateManager.Register(this);
            MakeActionEvent?.Invoke();
        }

        public void SetupAction(params object[] arguments)
        {
            Destination = (Vector3)arguments[0];
            MoveToDestination();
        }

        public event System.Action MakeActionEvent;

        public void MoveToDestination()
        {
            transform.position = Destination;
        }

    }
}