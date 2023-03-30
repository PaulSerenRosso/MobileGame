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

        public override void Evaluate()
        {
            _animator.SetBool(_data.NameParameter, _data.ValueToPass);
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