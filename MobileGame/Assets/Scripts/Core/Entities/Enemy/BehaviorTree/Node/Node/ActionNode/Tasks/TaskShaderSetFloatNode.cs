using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskShaderSetFloatNode : ActionNode
    {
        private TaskShaderSetFloatNodeSO _so;
        private TaskShaderSetFloatNodeDataSO _data;
        private EnemyManager _enemyManager;
        private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskShaderSetFloatNodeSO)nodeSO;
            _data = (TaskShaderSetFloatNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (_data.IsInternReferenceNameValue && _data.IsInternFloatValue)
            {
                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat((string)Sharer.InternValues[_so.InternValues[1].HashCode],
                        (float)Sharer.InternValues[_so.InternValues[0].HashCode]);
                } 
            }
            else if (_data.IsInternFloatValue)
            {
                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat(_data.ReferenceNameValue,
                        (float)Sharer.InternValues[_so.InternValues[0].HashCode]);
                }
            }
            else if (_data.IsInternReferenceNameValue)
            {
                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat((string)Sharer.InternValues[_so.InternValues[1].HashCode],
                        _data.FloatValue);
                }
            }
            else
            {
                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat(_data.ReferenceNameValue, _data.FloatValue);
                }
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