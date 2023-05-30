using System;
using System.Linq;
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
        public event Action ActivateFightCinematic;
        public event Action DeactivateFightCinematic;

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
        private const float _roundInitTimer = 2f;
        private CinematicFightManager _cinematicFightManager;
        private bool _isPlayerWon;
        private bool _isDebugFight;
        private bool _isTutorialFight;
        private GridSO _gridSo;
        private string _enemyAddressableName;
        private string _environmentAddressableName;
        private EnemyGlobalSO _enemyGlobalSO;

        public bool GetFightTutorial()
        {
            return _isTutorialFight;
        }

        public bool GetFightDebug()
        {
            return _isDebugFight;
        }

        public EnemyGlobalSO GetEnemySO()
        {
            return _enemyGlobalSO;
        }

        public void ActivatePause(Action callback)
        {
            ResetEntities();
            _playerController.LockController();
            _enemyManager.StopTree(callback);
            _hypeService.StopUltimateAreasIncreased();
            ActivatePauseEvent?.Invoke();
        }

        public void DeactivatePause()
        {
            _playerController.UnlockController();
            _hypeService.PlayUltimateAreasIncreased();
            _enemyManager.ReplayTree();
            DeactivatePauseEvent?.Invoke();
        }

        public void ActivatePausePlayer()
        {
            _playerController.LockController();
        }

        public void DeactivatePausePlayer()
        {
            _playerController.UnlockController();
        }

        public void StartFight(string environmentAddressableName, string enemyAddressableName, bool isDebugFight, bool isTutorialFight)
        {
            _isTutorialFight = isTutorialFight;
            if (_isTutorialFight)
            {
                _hypeService.SetStartHypeEnemy(_gameService.GlobalSettingsSO.StartHypeEnemyTutorial);
                _hypeService.SetStartHypePlayer(_gameService.GlobalSettingsSO.StartHypePlayerTutorial);
                EndFightEvent += QuitFightTutorial;
                _playerRoundCount = 1;
            }
            else
            {
                _hypeService.SetStartHypeEnemy(_gameService.GlobalSettingsSO.StartHypeEnemy);
                _hypeService.SetStartHypePlayer(_gameService.GlobalSettingsSO.StartHypePlayer);
            }

            _hypeService.EnabledService();
            _enemyAddressableName = enemyAddressableName;
            _environmentAddressableName = environmentAddressableName;
            _isDebugFight = isDebugFight;
            _hypeService.EnableHypeServiceEvent += GenerateFight;
            _canvasService.InitCanvasEvent += LoadCinematicFightManager;
        }


        private void LaunchEntryCinematic()
        {
            ActivateFightCinematic?.Invoke();
            ActivatePause(() => _cinematicFightManager.LaunchFightEntryCinematic(InitTimerRound));
        }

        private void LaunchUltimateEnemyCinematic()
        {
            ActivateFightCinematic?.Invoke();
            ActivatePause(() =>
            {
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
            ActivateFightCinematic?.Invoke();
            ActivatePause(() =>
            {
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
            ResetEntities();
            if (!_isDebugFight && !_isTutorialFight)
            {
                Fight currentFight = _tournamentService.GetCurrentFightPlayer();
                if (_isPlayerWon)
                {
                    currentFight.FightState = FightState.VICTORY;
                    switch (currentFight.TournamentStep)
                    {
                        case TournamentStep.QUARTER:
                            _tournamentService.SetPlayerCurrentFight(TournamentStep.DEMI);
                            break;
                        case TournamentStep.DEMI: 
                            _tournamentService.SetPlayerCurrentFight(TournamentStep.FINAL);
                            break;
                    }
                }
                else currentFight.FightState = FightState.DEFEAT;
            }
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
            _itemsService.SetPlayerItemLinker(_playerController.GetComponent<PlayerRenderer>().Animator.GetComponent<PlayerItemsLinker>());
            _itemsService.LinkItemPlayer();
            player.GetComponent<UltimatePlayerAction>().MakeActionEvent += LaunchUltimatePlayerCinematic;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(_enemyAddressableName, GenerateEnemy);
        }

        private void GenerateEnemy(GameObject gameObject)
        {
            var enemy = Object.Instantiate(gameObject);
            Release(gameObject);
            _enemyManager = enemy.GetComponent<EnemyManager>();
            _enemyGlobalSO = _gameService.GlobalSettingsSO.AllEnemyGlobalSO.First(enemy => enemy.enemyAdressableName == _enemyAddressableName);
            _enemyManager.CanUltimateEvent += LaunchUltimateEnemyCinematic;
            _playerController.SetupPlayer(_inputService, _tickeableService, _gridManager,
                _gridSo, _enemyManager, _hypeService);
            _enemyManager.Setup(_playerController.transform, _tickeableService, _gridManager, _poolService,
                _hypeService, _canvasService);
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
                _enemyManager.GetComponent<EnemyManager>().Animator, _playerController.transform,_enemyManager.transform, _enemyGlobalSO);
            LaunchEntryCinematic();
        }
        
        #endregion

        private void InitTimerRound()
        {
            ResetEntities();
            _tickTimerInitRound = new TickTimer(_roundInitTimer, _tickeableService.GetTickManager);
            _tickTimerInitRound.TickEvent += EndInitRound;
            _tickTimerInitRound.InitiateEvent += StartInitRound;
            _tickTimerInitRound.Initiate();
        }

        private void ResetRound()
        {
            ResetEntities();
            _tickTimerInitRound.Initiate();
        }

        private void StartInitRound()
        {
            DeactivateFightCinematic?.Invoke();
            if (_isTutorialFight) InitiateRoundEvent?.Invoke(-1); 
            else InitiateRoundEvent?.Invoke(_enemyRoundCount + _playerRoundCount);
        }

        private void EndInitRound()
        {
            DeactivatePause();
            EndInitiateRoundEvent?.Invoke();
        }

        public void QuitFight()
        {
            _playerRoundCount = 0;
            _enemyRoundCount = 0;
            _isPlayerWon = false;
            _cameraController.Unlink();
            ActivateFightCinematic = null;
            DeactivateFightCinematic = null;
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

        public void QuitFightTutorial(bool value)
        {
            if (_tickTimerInitRound != null)
            {
                _tickTimerInitRound.ResetEvents();
                _tickTimerInitRound.Cancel();
            }
            ActivatePause(QuitFight);
        }
    }
}