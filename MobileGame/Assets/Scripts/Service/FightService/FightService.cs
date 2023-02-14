using Addressables;
using Attributes;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service
{
    public class FightService : IFightService
    {
        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        private CameraController _cameraController;
        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;

        private EnvironmentSO _currentEnvironmentSO;

        // todo: launch fight(string soEnvironmentAddressable)
        // todo: get so environment
        // todo: get so grid generator
        // todo: generate grid
        // todo: generate environment
        // todo: generate player
        // todo: link player grid to input
        // todo: generate enemy

        public void StartFight(string environmentSOAdressableName)
        {
            _sceneService.LoadScene("GameScene");
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<EnvironmentSO>(environmentSOAdressableName,
                LoadEnvironmentSO);
        }

        private void LoadEnvironmentSO(EnvironmentSO so)
        {
            _currentEnvironmentSO = so;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(_currentEnvironmentSO.EnvironmentAddressableName,
                GenerateEnvironment);
        }

        private void GenerateEnvironment(GameObject gameObject)
        {
            var environment = Object.Instantiate(gameObject);
            Release(gameObject);
            environment.GetComponent<EnvironmentGridManager>().SetupGrid(
                _currentEnvironmentSO.GridOfEnvironment.CircleRadius,
                _currentEnvironmentSO.GridOfEnvironment.MovePoints,
                _currentEnvironmentSO.RendererMovePointAddressableName);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Player", GeneratePlayer);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Enemy", GenerateEnemy);
        }

        private void GeneratePlayer(GameObject gameObject)
        {
            var player = Object.Instantiate(gameObject);
            _playerManager = player.GetComponent<PlayerManager>();
            Release(gameObject);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Camera", GenerateCamera);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            Release(gameObject);
        }

        private void GenerateCamera(GameObject gameObject)
        {
            var camera = Object.Instantiate(gameObject);
            Release(gameObject);
            _cameraController = camera.GetComponent<CameraController>();
            _cameraController.PlayerManager = _playerManager;
            _canvasService.LoadInGameMenu();
        }
    }
}