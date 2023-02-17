using HelperPSR.MonoLoopFunctions;
using UnityEngine;

namespace Action
{
    public class MovementAction : MonoBehaviour, IAction, IUpdatable
    {
        public Vector3 Destination;
        public MovementSO MovementSo;

        private Vector3 _startPosition;
        private float _timer;
        private bool _isMoving;
        private float _ratioTime;

        public void OnUpdate()
        {
            if (_timer >= MovementSo.MaxTime)
            {
                _isMoving = false;
                transform.position = Destination;
                UpdateManager.UnRegister(this);
                return;
            }
            _timer += Time.deltaTime;
            _ratioTime = _timer / MovementSo.MaxTime;
            transform.position = Vector3.Lerp(_startPosition, Destination, MovementSo.CurvePosition.Evaluate(_ratioTime));
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        public void MakeAction()
        {
            _startPosition = transform.position;
            _isMoving = true;
            _timer = 0;
            UpdateManager.Register(this);
        }

        public void MoveToDestination()
        {
            transform.position = Destination;
        }
    }
}