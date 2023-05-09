using System;
using Addressables;
using Attributes;
using Environment.MoveGrid;
using HelperPSR.Tick;
using Player;
using Service.Hype;
using Service.Inputs;
using Service.Items;
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
        public event Action<bool> EndFightEvent;

        [DependsOnService] private IUICanvasSwitchableService _canvasService;
        [DependsOnService] private IInputService _inputService;
        [DependsOnService] private ITickeableService _tickeableService;
        [DependsOnService] private IPoolService _poolService;
        [DependsOnService] private IHypeService _hypeService;
        [DependsOnService] private IGameService _gameService;
        [DependsOnService] private ITournamentService _tournamentService;
        [DependsOnService] private IItemsService _itemsService;
        
        private const int _victoryRoundCount = 2;
        private CameraController _cameraController;
        private EnemyManager _enemyManager;
        private GridManager _gridManager;

        private PlayerController _playerController;
        private int _enemyRoundCount;
        private int _playerRoundCount;
        private TickTimer _tickTimerInitRound;
        private const float _roundInitTimer = 3f;
        private CinematicFightManager _cinematicFightManager;
        private bool _isPlayerWon;
        private bool _isDebugFight;
        private int _numberOfMatchsBeforePub = 3;
        private GridSO _gridSo;

        private string _enemyAddressableName;
        private string _environmentAddressableName;

        public void DecreasePub()
        {
            _numberOfMatchsBeforePub -= 1;
        }

        public void ResetPub()
        {
            _numberOfMatchsBeforePub = 1;
        }

        public int GetPub()
        {
            return _numberOfMatchsBeforePub;
        }
        
        private void ActivatePause(Action callback)
        {
            _playerController.LockController();
            _enemyManager.StopTree(callback);
            ActivatePauseEvent?.Invoke();
        }

        private void DeactivatePause()
        {
            _playerController.UnlockController();
            DeactivatePauseEvent?.Invoke();
        }

        public void StartFight(string environmentAddressableName, string enemyAddressableName, bool isDebugFight)
        {
            _hypeService.EnabledService();
            _enemyAddressableName = enemyAddressableName;
            _environmentAddressableName = environmentAddressableName;
            _isDebugFight = isDebugFight;
            _hypeService.EnableHypeServiceEvent += GenerateFight;
            _canvasService.InitCanvasEvent += LoadCinematicFightManager;
        }


        private void LaunchEntryCinematic()
        {
            ActivatePause(() => _cinematicFightManager.LaunchFightEntryCinematic(InitTimerRound));
        }

        private void LaunchUltimateEnemyCinematic()
        {
            ActivatePause(() =>
            {
                ResetEntities();
                _enemyRoundCount++;
                if (_enemyRoundCount == _victoryRoundCount)
                {
                    _cinematicFightManager.LaunchEnemyUltimateCinematic(EndFight);
                }
                else
                {
                    _cinematicFightManager.LaunchEnemyUltimateCinematic(ResetRound);
                }
            });
        }

        private void ResetEntities()
        {
            _gridManager.MoveGrid(new Vector3(0, 0, 0));
            _playerController.ResetPlayer();
            _enemyManager.ResetEnemy();
        }

        private void LaunchUltimatePlayerCinematic()
        {
            ActivatePause(() =>
            {
                ResetEntities();
                _playerRoundCount++;
                if (_playerRoundCount == _victoryRoundCount)
                {
                    _isPlayerWon = true;
                    _cinematicFightManager.LaunchPlayerUltimateCinematic(EndFight);
                }
                else
                {
                    _cinematicFightManager.LaunchPlayerUltimateCinematic(ResetRound);
                }
            });
        }

        private void EndFight()
        {
            EndFightEvent?.Invoke(_isPlayerWon);
        }

        #region Generate Fight

        private void GenerateFight()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(
                _environmentAddressableName,
                GenerateEnvironment);
        }

        private void GenerateEnvironment(GameObject gameObject)
        {
            var environment = Object.Instantiate(gameObject);
            _gridManager = environment.GetComponent<GridManager>();
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GridSO>("GridSO", LoadGridSO);
            Release(gameObject);
        }

        private void LoadGridSO(GridSO gridSo)
        {
            _gridSo = gridSo;
            _gridManager.SetupGrid(
                gridSo, GenerateFighters);
        }

        private void GenerateFighters()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Player", GeneratePlayer);
        }

        private void GeneratePlayer(GameObject gameObject)
        {
            var player = Object.Instantiate(gameObject);
            Release(gameObject);
            _playerController = player.GetComponent<PlayerController>();
            _itemsService.SetPlayerItemLinker(player.GetComponent<PlayerItemsLinker>());
            _itemsService.LinkItemPlayer();
            player.GetComponent<UltimatePlayerAction>().MakeActionEvent += LaunchUltimatePlayerCinematic;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(_enemyAddressableName, GenerateEnemy);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            Release(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            _enemyManager.CanUltimateEvent += LaunchUltimateEnemyCinematic;
            _playerController.SetupPlayer(_inputService, _tickeableService, _gridManager,
                _gridSo, _enemyManager, _hypeService);
            _enemyManager.Setup(_playerController.transform, _tickeableService, _gridManager, _poolService,
                _hypeService);
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

        private void LoadCinematicFightManager()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("CinematicFightManager",
                GenerateCinematicFightManager);
        }

        private void GenerateCinematicFightManager(GameObject gameObject)
        {
            var cinematicManager = Object.Instantiate(gameObject);
            Release(gameObject);
            _cinematicFightManager = cinematicManager.GetComponent<CinematicFightManager>();
            _cinematicFightManager.Init(_playerController.GetComponent<PlayerRenderer>().Animator,
                _enemyManager.GetComponent<EnemyManager>().Animator);
            LaunchEntryCinematic();
        }
        
        #endregion

        private void InitTimerRound()
        {
            _tickTimerInitRound = new TickTimer(_roundInitTimer, _tickeableService.GetTickManager);
            _tickTimerInitRound.TickEvent += EndInitRound;
            _tickTimerInitRound.InitiateEvent += StartInitRound;
            _tickTimerInitRound.Initiate();
        }

        private void ResetRound()
        {
            _tickTimerInitRound.Initiate();
        }

        private void StartInitRound()
        {
            InitiateRoundEvent?.Invoke(_enemyRoundCount + _playerRoundCount);
        }

        private void EndInitRound()
        {
            DeactivatePause();
            _enemyManager.ReplayTree();
            EndInitiateRoundEvent?.Invoke();
        }

        public void QuitFight()
        {
            if (!_isDebugFight)
            {
                if (_isPlayerWon)
                {
                    Fight currentFight = _tournamentService.GetCurrentFight();
                    currentFight._fightState = FightState.VICTORY;
                }
            }
            _playerRoundCount = 0;
            _enemyRoundCount = 0;
            _isPlayerWon = false;
            _cameraController.Unlink();
            InitiateRoundEvent = null;
            _canvasService.InitCanvasEvent -= LoadCinematicFightManager;
            EndInitiateRoundEvent = null;
            ActivatePauseEvent = null;
            DeactivatePauseEvent = null;
            EndFightEvent = null;
            _hypeService.DisabledService();
            _playerController.UnlinkPlayerController();
            _gameService.LoadMainMenuScene();
        }
    }
}