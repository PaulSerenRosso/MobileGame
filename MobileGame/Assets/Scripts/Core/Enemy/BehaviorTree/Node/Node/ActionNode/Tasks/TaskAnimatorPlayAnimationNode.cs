using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskAnimatorPlayAnimationNode : ActionNode
    {
        private TaskAnimatorPlayAnimationNodeSO _so;
        private TaskAnimatorPlayAnimationNodeDataSO _data;
        private Animator _animator;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskAnimatorPlayAnimationNodeSO)nodeSO;
            _data = (TaskAnimatorPlayAnimationNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _animator.Play(_data.NameParameter);
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