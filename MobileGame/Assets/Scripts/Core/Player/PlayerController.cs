using System.Collections.Generic;
using Environnement.MoveGrid;
using Player.Handler;
using Service.Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private List<SwipeSO> _allMovementSwipesSO;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerRotationHandler _playerRotationHandler;
        private IInputService _inputService;
        private EnemyController _enemyController;

        public void SetupPlayer(IInputService inputService, EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO, EnemyController enemyController)
        {
            _inputService = inputService;
            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                _inputService.AddSwipe(movementSwipeSO, _playerMovementHandler.TryMakeMovementAction);
            }
            _enemyController = enemyController;
            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index, _enemyController.transform);
            _playerRotationHandler.Setup(_enemyController.transform);
        }
    }
}