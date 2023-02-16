using HelperPSR.Singletons;
using Player;
using UnityEngine;

namespace Service
{
    public class CameraController : GenericSingleton<CameraController>
    {
        [HideInInspector] public PlayerController PlayerController;

        [Header("Camera Settings")] 
        [SerializeField] private Vector3 _positionBehind;
        [SerializeField] private Vector3 _positionTopDown;

        private bool _isBehind;

        private void Start()
        {
            transform.position = _positionBehind;
            transform.LookAt(PlayerController.transform);
            _isBehind = true;
        }

        private void Update()
        {
            UpdateCamera();
        }

        public void UpdateCamera()
        {
            if (!PlayerController) return;
            var positionCamera = PlayerController.transform.position + _positionBehind;
            transform.position = positionCamera;
        }

        public void SwapCamera()
        {
            if (_isBehind)
            {
                transform.position = _positionTopDown;
                transform.LookAt(PlayerController.transform);
                _isBehind = false;
            }
            else
            {
                transform.position = _positionBehind;
                transform.LookAt(PlayerController.transform);
                _isBehind = true;
            }
        }
    }
}