using HelperPSR.MonoLoopFunctions;
using UnityEngine;

namespace Action
{
    public class MovementAction : MonoBehaviour, IAction, IUpdatable
    {
        public Vector3 Destination;
        [SerializeField] private MovementSO _movementSO;

        private Vector3 _startPosition;
        private float _timer;
        private bool _isMoving;
        private float _ratioTime;

        public void OnUpdate()
        {
            if (_timer >= _movementSO.MaxTime)
            {
                _isMoving = false;
                transform.position = Destination;
                UpdateManager.UnRegister(this);
                return;
            }
            _timer += Time.deltaTime;
            _ratioTime = _timer / _movementSO.MaxTime;
            transform.position = Vector3.Lerp(_startPosition, Destination, _movementSO.CurvePosition.Evaluate(_ratioTime));
        }

        public bool IsInAction { get => _isMoving; }

        public void MakeAction()
        {
            _startPosition = transform.position;
            _isMoving = true;
            _timer = 0;
            UpdateManager.Register(this);
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