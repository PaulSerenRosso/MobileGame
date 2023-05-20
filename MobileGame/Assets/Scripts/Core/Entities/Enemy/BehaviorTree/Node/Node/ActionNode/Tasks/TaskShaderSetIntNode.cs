using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskShaderSetIntNode : ActionNode
    {
        private TaskShaderSetIntNodeSO _so;
        private TaskShaderSetIntNodeDataSO _data;
        private EnemyManager _enemyManager;
        private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskShaderSetIntNodeSO)nodeSO;
            _data = (TaskShaderSetIntNodeDataSO)_so.Data;
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
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
            _skinnedMeshRenderers = _enemyManager.GetSkinnedMeshRenderers();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}