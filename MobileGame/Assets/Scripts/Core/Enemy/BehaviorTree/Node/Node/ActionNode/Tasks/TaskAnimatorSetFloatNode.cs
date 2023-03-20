using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskAnimatorSetFloatNode : ActionNode
    {
        private TaskAnimatorSetFloatNodeSO _so;
        private TaskAnimatorSetFloatNodeDataSO _data;
        private Animator _animator;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskAnimatorSetFloatNodeSO)nodeSO;
            _data = (TaskAnimatorSetFloatNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _animator.SetFloat(_data.NameParameter, (float)Sharer.InternValues[_so.InternValues[0].HashCode]);
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