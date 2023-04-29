using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskIncreaseFloatNode : ActionNode
    {
        private TaskIncreaseFloatNodeSO _so;
        private TaskIncreaseFloatNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskIncreaseFloatNodeSO)nodeSO;
            _data = (TaskIncreaseFloatNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var result = _data.StartValue;
            if (Sharer.InternValues[_so.InternValues[0].HashCode] != null)
            {
                result = (float)Sharer.InternValues[_so.InternValues[0].HashCode] + (_data.FloatValue * Time.deltaTime);
            }
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, result);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}