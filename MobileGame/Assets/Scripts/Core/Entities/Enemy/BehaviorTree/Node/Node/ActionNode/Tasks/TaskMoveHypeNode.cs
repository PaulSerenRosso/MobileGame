using System;
using System.Collections.Generic;
using BehaviorTree.SO;
using BehaviorTree.SO.Actions;
using Service.Hype;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveHypeNode : ActionNode
    {
        private TaskMoveHypeNodeSO _so;
        private TaskMoveHypeNodeDataSO _data;
        private event Action<float> _moveHypeFunction;
        private event Func<float> _calculateMoveHypeAmountEvent;
        private IHypeService _hypeService;

        public override void Evaluate()
        {
            _moveHypeFunction?.Invoke(_calculateMoveHypeAmountEvent.Invoke());
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _hypeService = (IHypeService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.HypeService];
            switch (_data.HypeFunctionMode)
            {
                case BehaviorTreeEnums.HypeFunctionMode.DecreaseEnemy:
                {
                    _moveHypeFunction = _hypeService.DecreaseHypeEnemy;
                    break;
                }
                case BehaviorTreeEnums.HypeFunctionMode.DecreasePlayer:
                {
                    _moveHypeFunction = _hypeService.DecreaseHypePlayer;
                    break;
                }
                case BehaviorTreeEnums.HypeFunctionMode.IncreaseEnemy:
                {
                    _moveHypeFunction = _hypeService.IncreaseHypeEnemy;
                    break;
                }
                case BehaviorTreeEnums.HypeFunctionMode.IncreasePlayer:
                {
                    _moveHypeFunction = _hypeService.IncreaseHypePlayer;
                    break;
                }
                case BehaviorTreeEnums.HypeFunctionMode.SetEnemy:
                {
                    _moveHypeFunction = _hypeService.SetHypeEnemy;
                    break;
                }
                case BehaviorTreeEnums.HypeFunctionMode.SetPlayer:
                {
                    _moveHypeFunction = _hypeService.SetHypePlayer;
                    break;
                }
            }
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskMoveHypeNodeSO)nodeSO;
            _data = (TaskMoveHypeNodeDataSO)_so.Data;

            if (_data.IsUpdated)
            {
                _calculateMoveHypeAmountEvent += CalculateDecreaseAmountWithDeltatime;
            }
            else
            {
                _calculateMoveHypeAmountEvent += CalculateDecreaseAmount;
            }
        }

        private float CalculateDecreaseAmountWithDeltatime() => _data.HypeAmount * Time.deltaTime;

        private float CalculateDecreaseAmount() => _data.HypeAmount;
    }
}