using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetTimeAnimationNode : ActionNode
    {
        private TaskGetTimeAnimationNodeSO _so;
        private TaskGetTimeAnimationNodeDataSO _data;

        private Animator _animator;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetTimeAnimationNodeSO)nodeSO;
            _data = (TaskGetTimeAnimationNodeDataSO)_so.Data;
        }

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _animator.GetCurrentAnimatorStateInfo(0).length);
            yield return BehaviorTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _animator = (Animator)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Animator];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}