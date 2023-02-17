using System.Collections.Generic;
using Environnement.MoveGrid;
using Player.Movement;
using Service.Fight;
using Service.Inputs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private List<SwipeSO> _allMovementSwipesSO;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        private IInputService _inputService;

        public void SetupPlayer(IInputService inputService, EnvironmentGridManager environmentGridManager, EnvironmentSO environmentSO)
        {
            _inputService = inputService;
            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                _inputService.AddSwipe(movementSwipeSO, _playerMovementHandler.TryMakeMovementAction);
            }

            _playerMovementHandler.Setup(environmentGridManager, environmentSO.Index);
        }
    }
}