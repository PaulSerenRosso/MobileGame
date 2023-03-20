using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Service;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskInstantiateFXNode : ActionNode
    {
        private TaskInstantiateFXNodeSO _so;
        private TaskInstantiateFXNodeDataSO _data;
        private IPoolService _poolService;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskInstantiateFXNodeSO)nodeSO;
            _data = (TaskInstantiateFXNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            GameObject gameObject = _poolService.GetFromPool(_data.ParticleGO);
            gameObject.transform.position = (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode];
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _poolService = (IPoolService)externDependencyValues[BehaviourTreeEnums.TreeExternValues.PoolService];
            _poolService.CreatePool(_data.ParticleGO, _data.Count);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}