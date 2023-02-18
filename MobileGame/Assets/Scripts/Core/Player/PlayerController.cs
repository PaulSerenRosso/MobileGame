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
        private IInputService _inputService;
        private ITickeableService _tickeableService;
        private EnemyController _enemyController;

        public void SetupPlayer(IInputService inputService, ITickeableService tickeableService, EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO, EnemyController enemyController)
        {
            _inputService = inputService;
            _tickeableService = tickeableService;
            _enemyController = enemyController;
            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index, _inputService);
            _playerRotationHandler.Setup(_enemyController.transform);
            _playerAttackHandler.Setup(_inputService, _tickeableService.GetTickManager);
        }
    }
}