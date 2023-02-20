using Environnement.MoveGrid;
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
        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyController _enemyController;
        [SerializeField] private bool isMultiTapAttack;

        public void SetupPlayer(IInputService inputService, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO, EnemyController enemyController)
        {
            _inputService = inputService;
            _tickeableService = tickeableService;
            _enemyController = enemyController;
            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index, _inputService);
            _playerRotationHandler.Setup(_enemyController.transform);
            if (isMultiTapAttack)
            {
                _playerAttackMultiTapHandler.Setup(_inputService, _tickeableService.GetTickManager);
            }
            else
            {
                _playerAttackHandler.Setup(_inputService, _tickeableService.GetTickManager);
            }
            
            _playerTauntHandler.Setup(_inputService, _tickeableService.GetTickManager);
        }
    }
}