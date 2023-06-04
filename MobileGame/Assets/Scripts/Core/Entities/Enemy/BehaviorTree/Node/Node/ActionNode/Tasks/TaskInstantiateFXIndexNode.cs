using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Pool;
using Service;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskInstantiateFXIndexNode : ActionNode
    {
        private TaskInstantiateFXIndexNodeSO _so;
        private TaskInstantiateFXIndexNodeDataSO _data;
        private Pool<GameObject> _pool;
        private GridManager _gridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskInstantiateFXIndexNodeSO)nodeSO;
            _data = (TaskInstantiateFXIndexNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            GameObject gameObject = _pool.GetFromPool();
            var index = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            gameObject.transform.position = _gridManager.MovePoints[index].MeshRenderer.transform.position + new Vector3(0, 1, 0);
            _gridManager.StartCoroutine(_pool.AddToPoolLatter(gameObject, _data.Lifetime));
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _gridManager = (GridManager)externDependencyValues[BehaviorTreeEnums.TreeExternValues.GridManager];
            _pool = new Pool<GameObject>(_data.ParticleGO, _data.Count);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}