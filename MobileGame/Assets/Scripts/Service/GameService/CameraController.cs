using HelperPSR.Singletons;
using UnityEngine;

namespace Service
{
    public class CameraController : GenericSingleton<CameraController>
    {
        [HideInInspector] public PlayerManager PlayerManager;

        [Header("Camera Settings")] 
        [SerializeField] private Vector3 _positionBehind;
        [SerializeField] private Vector3 _positionTopDown;

        private bool _isBehind;

        private void Start()
        {
            transform.position = _positionBehind;
            transform.LookAt(PlayerManager.transform);
            _isBehind = true;
        }

        private void Update()
        {
            UpdateCamera();
        }

        public void UpdateCamera()
        {
            if (!PlayerManager) return;
            var positionCamera = PlayerManager.transform.position + _positionBehind;
            transform.position = positionCamera;
        }

        public void SwapCamera()
        {
            if (_isBehind)
            {
                transform.position = _positionTopDown;
                transform.LookAt(PlayerManager.transform);
                _isBehind = false;
            }
            else
            {
                transform.position = _positionBehind;
                transform.LookAt(PlayerManager.transform);
                _isBehind = true;
            }
        }
    }
}