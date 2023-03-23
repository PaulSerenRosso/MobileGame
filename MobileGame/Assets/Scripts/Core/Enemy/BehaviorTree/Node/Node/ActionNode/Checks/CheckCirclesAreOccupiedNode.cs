using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckCirclesAreOccupiedNode : ActionNode
    {
        private CheckCirclesAreOccupiedNodeSO _so;
        private CheckCirclesAreOccupiedNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (!_environmentGridManager.CheckIfOneMovePointInCirclesIsOccupied(_data.CircleIndexes, (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode]))
            {
             
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }
            return BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so =(CheckCirclesAreOccupiedNodeSO) nodeSO;
            _data = (CheckCirclesAreOccupiedNodeDataSO)_so.Data;
        }

        public override void SetDependencyValues(Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues, Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =(EnvironmentGridManager)
                externDependencyValues[BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager];
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}