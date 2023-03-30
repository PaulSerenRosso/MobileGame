using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerDirectionNode : ActionNode
    {
        private CheckPlayerDirectionNodeSO _so;
        private CheckPlayerDirectionNodeDataSO _data;

        private Transform _transform;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerDirectionNodeSO)nodeSO;
            _data = (CheckPlayerDirectionNodeDataSO)_so.Data;
        }

        public override IEnumerator Evaluate()
        {
            float angle = Vector3.Angle(_transform.forward, (Vector3) Sharer.InternValues[_so.InternValues[0].HashCode]);
            State = angle < 10 ? BehaviorTreeEnums.NodeState.SUCCESS : BehaviorTreeEnums.NodeState.FAILURE;
                yield break;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _transform = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}