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

        public override void Evaluate()
        {
            base.Evaluate();
            GameObject gameObject = _poolService.GetFromPool(_data.ParticleGO);
            gameObject.transform.position = (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode];
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _poolService = (IPoolService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PoolService];
            _poolService.CreatePool(_data.ParticleGO, _data.Count);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}