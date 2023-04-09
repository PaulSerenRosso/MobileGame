using Addressables;
using Attributes;
using Environment.MoveGrid;
using Player;
using Service.Hype;
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
        [DependsOnService] private IPoolService _poolService;
        [DependsOnService] private IHypeService _hypeService;
        
        private CameraController _cameraController;
        private EnemyManager _enemyManager;
        private EnvironmentGridManager _environmentGridManager;
        private EnvironmentSO _currentEnvironmentSO;
        private PlayerController _playerController;
        private int _rounds;

        public void StartFight(string environmentAddressableName)
        {
            _sceneService.LoadScene("GameScene");
            _hypeService.EnabledService();
            _rounds = 0;
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
               _currentEnvironmentSO,() => GenerateFighters(gameObject));
        }

        private void GenerateFighters(GameObject gameObject)
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Player", GeneratePlayer);
            Release(gameObject);
        }

        private void GeneratePlayer(GameObject gameObject)
        {
            var player = Object.Instantiate(gameObject);
            Release(gameObject);
            _playerController = player.GetComponent<PlayerController>();
            player.GetComponent<UltimatePlayerAction>().MakeActionEvent += ResetRound;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Enemy", GenerateEnemy);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            Release(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            _playerController.SetupPlayer(_inputService, _tickeableService, _environmentGridManager, _currentEnvironmentSO, _enemyManager, _hypeService);
            _enemyManager.Setup(_playerController.transform, _tickeableService, _environmentGridManager, _poolService, _hypeService);
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Camera", GenerateCamera);
        }

        private void GenerateCamera(GameObject gameObject)
        {
            var camera = Object.Instantiate(gameObject);
            Release(gameObject);
            _cameraController = camera.GetComponent<CameraController>();
            _cameraController.Setup(_playerController.transform, _enemyManager.transform);
            _canvasService.LoadInGameMenu();
        }

        private void ResetRound()
        {
            if (_rounds + 1 > _enemyManager.EnemySO.Rounds)
            {
                // TODO : Return in the menu
            }

            _rounds++;
            // TODO : Get the hype by default of the player and the enemy
            _playerController.ResetPlayer(30);
            _enemyManager.ResetEnemy(30);
        }
    }
}