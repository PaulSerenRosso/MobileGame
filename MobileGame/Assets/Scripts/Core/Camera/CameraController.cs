using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using UnityEngine;

namespace Service
{
    public class CameraController : MonoBehaviour, IUpdatable, IRemoteConfigurable
    {
        [Header("Camera Settings")]
        [SerializeField] private CameraSettingsSO _cameraSettingsSO;

        private Transform _player;
        private Transform _enemy;

        public void OnUpdate()
        {
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            if (!_player || !_enemy) return;
            transform.position = Vector3.Lerp(transform.position, _player.position + _player.TransformPoint(_cameraSettingsSO.Offset), _cameraSettingsSO.SpeedPosition * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_enemy.position - transform.position), _cameraSettingsSO.SpeedRotation * Time.deltaTime);
        }

        public void Setup(Transform player, Transform enemy)
        {
            _player = player;
            _enemy = enemy;
            transform.position = _player.position + _player.TransformPoint(_cameraSettingsSO.Offset);
            transform.LookAt(_enemy);
            UpdateManager.Register(this);
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }
        public void SetRemoteConfigurableValues()
        {
            _cameraSettingsSO.SpeedPosition = RemoteConfigManager.Config.GetFloat("CameraSpeedPosition");
           _cameraSettingsSO.SpeedRotation = RemoteConfigManager.Config.GetFloat("CameraSpeedRotation");
        }
    }
}