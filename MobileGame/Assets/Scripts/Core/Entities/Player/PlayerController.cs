using System.Collections.Generic;
using System.Linq;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using Player.Handler;
using Service.Hype;
using Service.Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IUpdatable
    {
        [SerializeField] private PlayerAttackHandler _playerAttackHandler;
        [SerializeField] private PlayerUltimateHandler playerUltimateHandler;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerRenderer _playerRenderer;
        [SerializeField] private PlayerRotationHandler _playerRotationHandler;
        [SerializeField] private PlayerTauntHandler _playerTauntHandler;

        [SerializeField] private float _timeStunAvailable;
        [SerializeField] private float _timerStun;
        [SerializeField] private float _percentageHealthStun;

        private float _timer;
        private bool _isStun;
        
        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyManager _enemyManager;
        private GridSO _gridSO;
        private IHypeService _hypeService;
        private bool _isLocked;
        private List<EntityStunTrigger> _currentStunTriggers;

        private void OnEnable()
        {
            _currentStunTriggers = new List<EntityStunTrigger>();
            UpdateManager.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.UnRegister(this);
        }
        
        // TODO : Add feedback on stun
        
        public void OnUpdate()
        {
            TimerStun();
            if (_currentStunTriggers.Count < 1 || _isStun) return;
            _currentStunTriggers.RemoveAll(entityStunTrigger => entityStunTrigger.Time > _timeStunAvailable);
            foreach (var entityStunTrigger in _currentStunTriggers.Where(entityStunTrigger =>
                         entityStunTrigger.Time < _timeStunAvailable))
            {
                entityStunTrigger.Time += Time.deltaTime;
            }
            
            if (!_currentStunTriggers.Any(entityStunTrigger =>
                    (entityStunTrigger.DamageAmount / _hypeService.GetMaximumHype()) >=
                    _percentageHealthStun)) return;
            Debug.Log("Player is stun");
            LockController();
            _isStun = true;
            _currentStunTriggers.Clear();
        }
        
        private void TimerStun()
        {
            if (!_isStun) return;
            _timer += Time.deltaTime;
            if (_timer >= _timerStun)
            {
                Debug.Log("Player is free");
                UnlockController();
                _timer = 0;
                _isStun = false;
                _currentStunTriggers.Clear();
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
            _hypeService.GetPlayerDecreaseHypeEvent += TakeStun;
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
            playerUltimateHandler.AddCondition(CheckIsLockedController);
            playerUltimateHandler.Setup(hypeService);
            _playerRenderer.Init();
        }

        public void UnlinkPlayerController()
        {
            _playerAttackHandler.Unlink();
            playerUltimateHandler.Unlink();
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
        }

        public void UnlockController()
        {
            _isLocked = false;
        }
        
        private void TakeStun(float amount)
        {
            _currentStunTriggers.Add(new EntityStunTrigger(0, amount));
            foreach (var entityStunTrigger in _currentStunTriggers.Where(entityStunTrigger =>
                         entityStunTrigger.Time < _timeStunAvailable))
            {
                entityStunTrigger.DamageAmount += amount;
            }
        }
    }
}