using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Object;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskAnimatorSetIntNode : ActionNode
    {
        private TaskAnimatorSetIntNodeSO _so;
        private TaskAnimatorSetIntNodeDataSO _data;
        private Animator _animator;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskAnimatorSetIntNodeSO)nodeSO;
            _data = (TaskAnimatorSetIntNodeDataSO)_so.Data;
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
                if (_data.IsValueIntern)
                    _animator.SetInteger(_data.NameParameter, (int)Sharer.InternValues[_so.InternValues[0].HashCode]);
                else _animator.SetInteger(_data.NameParameter, _data.ValueToPass);
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