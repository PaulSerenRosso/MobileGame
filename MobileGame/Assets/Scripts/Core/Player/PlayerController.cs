using Environment.MoveGrid;
using Player.Handler;
using Service.Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerRotationHandler _playerRotationHandler;
        [SerializeField] private PlayerAttackHandler _playerAttackHandler;
        [SerializeField] private PlayerAttackMultiTapHandler _playerAttackMultiTapHandler;
        [SerializeField] private PlayerTauntHandler _playerTauntHandler;
        [SerializeField] private PlayerRenderer _playerRenderer;
        [SerializeField] private bool _isMultiTapAttack;
        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyManager _enemyManager;

        public void SetupPlayer(IInputService inputService, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO, EnemyManager enemyManager)
        {
            _inputService = inputService;
            _tickeableService = tickeableService;
            _enemyManager = enemyManager;
            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index, _inputService);
            _playerRotationHandler.Setup(_enemyManager.transform);
            if (_isMultiTapAttack)
            {
                _playerAttackMultiTapHandler.Setup(_inputService, _tickeableService.GetTickManager);
            }
            else
            {
                _playerAttackHandler.Setup(_inputService, _tickeableService.GetTickManager);
            }

            _playerTauntHandler.Setup(_inputService, _tickeableService.GetTickManager);
            _playerRenderer.Init();
        }
    }
}