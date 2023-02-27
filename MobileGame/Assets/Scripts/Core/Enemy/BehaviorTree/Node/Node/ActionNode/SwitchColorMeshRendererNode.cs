using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class SwitchColorMeshRendererNode : ActionNode
    {
        private SwitchColorMeshRendererNodeSO _so;
        private SwitchColorMeshRendererNodeDataSO _dataSo;
        private MeshRenderer _meshRenderer;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (SwitchColorMeshRendererNodeSO)nodeSO;
            _dataSo = (SwitchColorMeshRendererNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _meshRenderer.material.color = _dataSo.SwitchableColor;
            Debug.Log($"color: {_dataSo.SwitchableColor}");
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
            return _dataSo;
        }
    }
}