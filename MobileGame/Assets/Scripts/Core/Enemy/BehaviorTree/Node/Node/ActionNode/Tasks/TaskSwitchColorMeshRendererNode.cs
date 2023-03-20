using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSwitchColorMeshRendererNode : ActionNode
    {
        private TaskSwitchColorMeshRendererNodeSO _so;
        private TaskSwitchColorMeshRendererNodeDataSO _data;
        private MeshRenderer _meshRenderer;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSwitchColorMeshRendererNodeSO)nodeSO;
            _data = (TaskSwitchColorMeshRendererNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _meshRenderer.material.color = _data.SwitchableColor;
            return BehaviourTreeEnums.NodeState.RUNNING;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _meshRenderer = (MeshRenderer)enemyDependencyValues[BehaviourTreeEnums.TreeEnemyValues.MeshRenderer];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}