using System;
using Addressables;
using Attributes;
using Environment.MoveGrid;
using HelperPSR.Tick;
using Player;
using Service.Hype;
using Service.Inputs;
using Service.UI;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;
using Object = UnityEngine.Object;

namespace Service.Fight
{
    public class FightService : IFightService
    {
        public event Action<int> InitiateRoundEvent;
        public event Action EndInitiateRoundEvent;
        public event Action ActivatePauseEvent;
        public event Action DeactivatePauseEvent;

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
        private int _roundCount;
        private TickTimer _tickTimerInitRound;
        private const float _roundInitTimer = 3f;
        private CinematicFightManager _cinematicFightManager;

        private void ActivatePause()
        {
            _hypeService.DeactivateDecreaseUpdateHypePlayer();
            _playerController.LockController();
            _enemyManager.StopTree();
            ActivatePauseEvent?.Invoke();
        }

        private void DeactivatePause()
        {
            _hypeService.ActivateDecreaseUpdateHypePlayer();
            _playerController.UnlockController();
            DeactivatePauseEvent?.Invoke();
        }
        
        public void StartFight(string environmentAddressableName)
        {
            _sceneService.LoadScene("GameScene");
            _hypeService.EnabledService();
            _roundCount = 0;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<EnvironmentSO>(environmentAddressableName,
                LoadEnvironmentSO);
            _canvasService.InitCanvasEvent += LaunchEntryCinematic;
        }

        private void LaunchEntryCinematic()
        {
            ActivatePause();
            _cinematicFightManager.LaunchFightEntryCinematic(InitTimerRound);
        }

        private void LaunchUltimateEnemyCinematic()
        {
            ActivatePause();
            _cinematicFightManager.LaunchEnemyUltimateCinematic(ResetRound);
        }

        private void LaunchUltimatePlayerCinematic()
        {
            ActivatePause();
            _cinematicFightManager.LaunchPlayerUltimateCinematic(ResetRound);
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
            player.GetComponent<UltimatePlayerAction>().MakeActionEvent += LaunchUltimatePlayerCinematic;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Enemy", GenerateEnemy);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            Release(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            _enemyManager.CanUltimateEvent += LaunchUltimateEnemyCinematic;
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
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("CinematicFightManager", GenerateCinematicFightManager);
        }

        private void GenerateCinematicFightManager(GameObject gameObject)
        {
            var cinematicManager = Object.Instantiate(gameObject);
            Release(gameObject);
            _cinematicFightManager = cinematicManager.GetComponent<CinematicFightManager>();
            _cinematicFightManager.Init(_playerController.GetComponent<PlayerRenderer>().Animator, _enemyManager.Animator);
        }
        private void InitTimerRound()
        {
            _tickTimerInitRound = new TickTimer(_roundInitTimer, _tickeableService.GetTickManager);
            _tickTimerInitRound.TickEvent += EndInitRound;
            _tickTimerInitRound.InitiateEvent += StartInitRound;
            _tickTimerInitRound.Initiate();
        }

        private void ResetRound()
        {
            if (_roundCount + 1 > _enemyManager.EnemySO.Rounds)
            {
                // TODO : Return in the menu
            }
            _roundCount++;
            _environmentGridManager.MoveGrid(new Vector3(0, 0, 0));
            _playerController.ResetPlayer();
            _enemyManager.ResetEnemy();
            _tickTimerInitRound.Initiate();
        }

        private void StartInitRound()
        {
            InitiateRoundEvent?.Invoke(_roundCount);
        }

        private void EndInitRound()
        {
            DeactivatePause();
            _enemyManager.ResetTree();
            EndInitiateRoundEvent?.Invoke();
        }
    }
}