using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using Service;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskInstantiateFXNode : ActionNode
    {
        private TaskInstantiateFXNodeSO _so;
        private TaskInstantiateFXNodeDataSO _data;
        private IPoolService _poolService;
        private GridManager _gridManager;

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
            var index = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            gameObject.transform.position = _gridManager.MovePoints[index].MeshRenderer.transform.position + new Vector3(0, 1, 0);
            _poolService.AddToPoolLater(_data.ParticleGO, gameObject, 5f);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _gridManager = (GridManager)externDependencyValues[BehaviorTreeEnums.TreeExternValues.GridManager];
            _poolService = (IPoolService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PoolService];
            _poolService.CreatePool(_data.ParticleGO, _data.Count);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}