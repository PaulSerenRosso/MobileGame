using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskAnimatorSetBoolNode : ActionNode
    {
        private TaskAnimatorSetBoolNodeSO _so;
        private TaskAnimatorSetBoolNodeDataSO _data;
        private Animator _animator;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskAnimatorSetBoolNodeSO)nodeSO;
            _data = (TaskAnimatorSetBoolNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _animator.SetBool(_data.NameParameter, _data.ValueToPass);
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }
        
        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _animator =
                (Animator)enemyDependencyValues[
                    BehaviourTreeEnums.TreeEnemyValues.Animator];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}