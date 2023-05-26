using Core.Entities;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using Player.Handler;
using Service.Hype;
using Service.Inputs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour, IUpdatable
    {
        [SerializeField] private PlayerAttackHandler _playerAttackHandler;
        [FormerlySerializedAs("playerUltimateHandler")] [SerializeField] private PlayerUltimateHandler _playerUltimateHandler;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerRenderer _playerRenderer;
        [SerializeField] private PlayerRotationHandler _playerRotationHandler;
        [SerializeField] private PlayerTauntHandler _playerTauntHandler;

        [FormerlySerializedAs("_timerStun")] [SerializeField] private float _timeStun;
        [SerializeField] private float _timeInvunerable;

        private float _timerStun;
        private float _timerInvulnerable;
        private bool _isStun;
        private bool _isInvulnerable;
        
        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyManager _enemyManager;
        private GridSO _gridSO;
        private IHypeService _hypeService;
        private bool _isLocked;

        private void OnEnable()
        {
            UpdateManager.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.UnRegister(this);
        }

        public void OnUpdate()
        {
            if (_isStun) TimerStun();
            if (_isInvulnerable) TimerInvunerable();
        }
        
        private void TimerStun()
        {
            _timerStun += Time.deltaTime;
            if (_timerStun >= _timeStun)
            {
                _playerRenderer.DeactivateStunFeedback();
                UnlockController();
                _timerStun = 0;
                _isStun = false;
                _isInvulnerable = true;
            }
        }
        
        private void TimerInvunerable()
        {
            _timerInvulnerable += Time.deltaTime;
            if (_timerInvulnerable >= _timeInvunerable)
            {
                _playerRenderer.DeactivateStunFeedback();
                UnlockController();
                _timerInvulnerable = 0;
                _isStun = false;
                _isInvulnerable = false;
            }
        }


        public void SetupPlayer(IInputService inputService, ITickeableService tickeableService,
            GridManager gridManager, GridSO gridSO, EnemyManager enemyManager,
            IHypeService hypeService)
        {
            _inputService = inputService;
            _tickeableService = tickeableService;
            _enemyManager = enemyManager;
            _hypeService = hypeService;
            _gridSO = gridSO;
            _playerMovementHandler.AddCondition(CheckIsLockedController);
            _playerMovementHandler.Setup(gridManager, gridSO.Index, _inputService,
                _tickeableService.GetTickManager);
            _playerRotationHandler.AddCondition(CheckIsLockedController);
            _playerRotationHandler.Setup(_enemyManager.transform);
            _playerAttackHandler.AddCondition(CheckIsLockedController);
            _playerAttackHandler.Setup(_inputService, _tickeableService.GetTickManager,
                _enemyManager.GetComponent<IHypeable>(),
                gridManager, _hypeService);
            _playerTauntHandler.AddCondition(CheckIsLockedController);
            _playerTauntHandler.Setup(_inputService, _tickeableService.GetTickManager, _hypeService);
            _playerTauntHandler.MakeActionEvent += _playerRenderer.ActivateTauntFX;
            _playerTauntHandler.MakeFinishActionEvent += _playerRenderer.DeactivateTauntFX;
            _playerUltimateHandler.AddCondition(CheckIsLockedController);
            _playerUltimateHandler.Setup(_hypeService);
            _playerRenderer.Init();
        }

        public void UnlinkPlayerController()
        {
            _playerAttackHandler.Unlink();
            _playerUltimateHandler.Unlink();
            _playerRotationHandler.Unlink();
            _playerMovementHandler.Unlink();
            _playerTauntHandler.Unlink();
        }

        private bool CheckIsLockedController()
        {
            return !_isLocked;
        }

        public void ResetPlayer()
        {
            _playerMovementHandler.ResetMovePoint(_gridSO.Index);
            transform.rotation = Quaternion.identity;
            _hypeService.ResetHypePlayer();
        }

        public void LockController()
        {
            _isLocked = true;
            _playerTauntHandler.TryCancelTaunt();
        }

        public void UnlockController()
        {
            _isLocked = false;
        }
        
        public void TakeStun()
        {
            if (_isStun || _isInvulnerable) return;
            _playerRenderer.ActivateStunFeedback();
            LockController();
            _isStun = true;
        }
    }
}