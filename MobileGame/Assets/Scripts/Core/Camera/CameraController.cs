using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using UnityEngine;

namespace Service
{
    public class CameraController : MonoBehaviour, IFixedUpdate, IRemoteConfigurable
    {
        [Header("Camera Settings")] [SerializeField]
        private CameraSettingsSO _cameraSettingsSO;

        private Transform _player;
        private Transform _enemy;

        public void OnFixedUpdate()
        {
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            if (!_player || !_enemy) return;
            transform.position = Vector3.Lerp(transform.position, 
                _player.TransformPoint(_cameraSettingsSO.Offset),
                _cameraSettingsSO.SpeedPosition * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation((_enemy.position - transform.position).normalized),
                _cameraSettingsSO.SpeedRotation * Time.fixedDeltaTime);
        }

        public void Setup(Transform player, Transform enemy)
        {
            _player = player;
            _enemy = enemy;
            transform.position = _player.position + _player.TransformPoint(_cameraSettingsSO.Offset);
            transform.LookAt(_enemy);
            FixedUpdateManager.Register(this);
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }

        public void SetRemoteConfigurableValues()
        {
            _cameraSettingsSO.SpeedPosition = RemoteConfigManager.Config.GetFloat("CameraSpeedPosition");
            _cameraSettingsSO.SpeedRotation = RemoteConfigManager.Config.GetFloat("CameraSpeedRotation");
        }
    }
}