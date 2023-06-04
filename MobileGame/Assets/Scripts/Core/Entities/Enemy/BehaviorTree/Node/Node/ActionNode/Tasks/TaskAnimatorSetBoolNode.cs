using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Object;
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
            base.Evaluate();
            if (_animator.HasParameter(_data.NameParameter))
            {
                if (_data.IsValueIntern) _animator.SetBool(_data.NameParameter, (bool)Sharer.InternValues[_so.InternValues[0].HashCode]);
                else  _animator.SetBool(_data.NameParameter, _data.ValueToPass);
            }
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