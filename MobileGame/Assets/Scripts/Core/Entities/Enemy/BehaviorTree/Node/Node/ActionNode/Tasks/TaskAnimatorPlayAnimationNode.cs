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

        public override void Evaluate()
        {
            base.Evaluate();
            _animator.Play(_data.NameParameter);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _animator =
                (Animator)enemyDependencyValues[
                    BehaviorTreeEnums.TreeEnemyValues.Animator];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}