using Addressables;
using Attributes;
using Environnement.MoveGrid;
using Player;
using Service.Inputs;
using Service.UI;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service.Fight
{
    public class FightService : IFightService
    {
        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        [DependsOnService] private IInputService _inputService;

        [DependsOnService] private ITickeableService _tickeableService;

        private CameraController _cameraController;
        private PlayerController _playerController;
        private EnemyManager _enemyManager;
        private EnvironmentGridManager _environmentGridManager;

        private EnvironmentSO _currentEnvironmentSO;

        // todo: launch fight(string soEnvironmentAddressable)
        // todo: get so environment
        // todo: get so grid generator
        // todo: generate grid
        // todo: generate environment
        // todo: generate player
        // todo: link player grid to input
        // todo: generate enemy

        public void StartFight(string environmentAddressableName)
        {
            _sceneService.LoadScene("GameScene");
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<EnvironmentSO>(environmentAddressableName,
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
            _environmentGridManager = environment.GetComponent<EnvironmentGridManager>();
            _environmentGridManager.SetupGrid(
                _currentEnvironmentSO.GridOfEnvironment.CircleRadius,
                _currentEnvironmentSO.GridOfEnvironment.MovePoints,
                _currentEnvironmentSO.RendererMovePointAdressableName,() => GenerateFighters(gameObject));
        }

        private void GenerateFighters(GameObject gameObject)
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Player", GeneratePlayer);
            Release(gameObject);
        }

        private void GeneratePlayer(GameObject gameObject)
        {
            var player = Object.Instantiate(gameObject);
            _playerController = player.GetComponent<PlayerController>();
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Enemy", GenerateEnemy);
            Release(gameObject);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            _playerController.SetupPlayer(_inputService, _tickeableService, _environmentGridManager, _currentEnvironmentSO, _enemyManager);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Camera", GenerateCamera);
            Release(gameObject);
        }

        private void GenerateCamera(GameObject gameObject)
        {
            var camera = Object.Instantiate(gameObject);
            Release(gameObject);
            _cameraController = camera.GetComponent<CameraController>();
            _cameraController.Setup(_playerController.transform, _enemyManager.transform);
            _canvasService.LoadInGameMenu();
        }
    }
}