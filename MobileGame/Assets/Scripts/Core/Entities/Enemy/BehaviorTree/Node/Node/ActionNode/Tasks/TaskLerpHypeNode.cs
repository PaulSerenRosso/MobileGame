using System;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Service.Hype;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskLerpHypeNode : ActionNode
    {
        private TaskLerpHypeNodeSO _so;
        private TaskLerpHypeNodeDataSO _data;
        private IHypeService _hypeService;

        private event Func<float> _getHypeValueEvent;
        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskLerpHypeNodeSO)nodeSO;
            _data = (TaskLerpHypeNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var lerpHype = Mathf.Lerp(0, _hypeService.GetMaximumHype(), _getHypeValueEvent.Invoke());
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, lerpHype);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        
        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _hypeService = (IHypeService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.HypeService];
            if (_data.isPlayerHype)
                _getHypeValueEvent = _hypeService.GetCurrentHypePlayer;
            else
            {
                _getHypeValueEvent = _hypeService.GetCurrentHypeEnemy;
            }
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}