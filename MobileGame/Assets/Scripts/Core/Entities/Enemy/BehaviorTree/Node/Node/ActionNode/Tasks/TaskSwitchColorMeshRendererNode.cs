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

        public override void Evaluate()
        {
            base.Evaluate();
            // if (_meshRenderer) _meshRenderer.material.color = _data.SwitchableColor;
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            // _meshRenderer = (MeshRenderer)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.MeshRenderer];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}