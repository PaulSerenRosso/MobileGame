using Environment.MoveGrid;
using Player.Handler;
using Service.Hype;
using Service.Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerAttackHandler _playerAttackHandler;
        [SerializeField] private PlayerUltimateHandler playerUltimateHandler;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerRenderer _playerRenderer;
        [SerializeField] private PlayerRotationHandler _playerRotationHandler;
        [SerializeField] private PlayerTauntHandler _playerTauntHandler;

        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyManager _enemyManager;
        private EnvironmentSO _environmentSO;
        private IHypeService _hypeService;
        private bool _isLocked;

        public void SetupPlayer(IInputService inputService, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO, EnemyManager enemyManager,
            IHypeService hypeService)
        {
            _inputService = inputService;
            _tickeableService = tickeableService;
            _enemyManager = enemyManager;
            _hypeService = hypeService;
            _environmentSO = environmentSO;
            _playerMovementHandler.AddCondition(CheckIsLockedController);
            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index, _inputService,
                _tickeableService.GetTickManager);
            _playerRotationHandler.AddCondition(CheckIsLockedController);
            _playerRotationHandler.Setup(_enemyManager.transform);
            _playerAttackHandler.AddCondition(CheckIsLockedController);
            _playerAttackHandler.Setup(_inputService, _tickeableService.GetTickManager,
                _enemyManager.GetComponent<IHypeable>(),
                environmentGridManager, _hypeService);
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
            _playerMovementHandler.ResetMovePoint(_environmentSO.Index);
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
    }
}