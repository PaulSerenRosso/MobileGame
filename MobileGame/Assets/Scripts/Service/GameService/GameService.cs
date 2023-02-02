using Addressables;
using Attributes;
using Service.AudioService;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private IAudioService _audioService;

        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        private PlayerManager _playerManager;
        private CameraController _cameraController;

        [ServiceInit]
        private void Initialize()
        {
            _canvasService.LoadMainMenu();
        }

        public void StartGame()
        {
            _sceneService.LoadScene("GameScene");
            // Instantiate environment
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Environment", GenerateEnvironment);
        }

        private void GenerateEnvironment(GameObject gameObject)
        {
            var environment = Object.Instantiate(gameObject);
            // Release(environment);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Player", GeneratePlayer);
        }

        private void GenerateCamera(GameObject gameObject)
        {
            var camera = Object.Instantiate(gameObject);
            _cameraController = camera.GetComponent<CameraController>();
            _cameraController.PlayerManager = _playerManager;
        }

        private void GeneratePlayer(GameObject gameObject)
        {
            var player = Object.Instantiate(gameObject);
            _playerManager = player.GetComponent<PlayerManager>();
            // Release(player);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Camera", GenerateCamera);
        }
    }
}