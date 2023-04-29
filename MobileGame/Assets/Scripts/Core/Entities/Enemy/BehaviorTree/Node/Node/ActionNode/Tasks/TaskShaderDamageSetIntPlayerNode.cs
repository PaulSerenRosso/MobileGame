using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskShaderDamageSetIntPlayerNode : ActionNode
    {
        private TaskShaderDamageSetIntPlayerNodeSO _so;
        private TaskShaderDamageSetIntPlayerNodeDataSO _data;
        private PlayerRenderer _playerRenderer;
        private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskShaderDamageSetIntPlayerNodeSO)nodeSO;
            _data = (TaskShaderDamageSetIntPlayerNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetInt(_data.ReferenceNameValue, _data.IntValue);
            }
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerRenderer = (PlayerRenderer)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerRenderer];
            _skinnedMeshRenderers = _playerRenderer.GetSkinnedMeshRenderers();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}