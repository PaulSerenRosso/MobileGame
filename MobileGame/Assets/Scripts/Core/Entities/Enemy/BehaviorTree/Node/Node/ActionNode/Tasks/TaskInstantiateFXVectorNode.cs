using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Pool;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskInstantiateFXVectorNode : ActionNode
    {
        private TaskInstantiateFXVectorNodeSO _so;
        private TaskInstantiateFXVectorNodeDataSO _data;
        private Pool<GameObject> _pool;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskInstantiateFXVectorNodeSO)nodeSO;
            _data = (TaskInstantiateFXVectorNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            Vector3[] movePointPositions = ((List<Vector3>)Sharer.InternValues[_so.InternValues[0].HashCode]).ToArray();
            for (int i = 0; i < movePointPositions.Length; i++)
            {
                GameObject gameObject = _pool.GetFromPool();
                gameObject.transform.position = movePointPositions[i];
                _pool.AddToPoolLatter(gameObject, 5f);
            }
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _pool = new Pool<GameObject>(_data.ParticleGO, _data.Count);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}