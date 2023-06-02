using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Pool;
using Service.Fight;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskInstantiateFXVectorNode : ActionNode
    {
        private TaskInstantiateFXVectorNodeSO _so;
        private TaskInstantiateFXVectorNodeDataSO _data;
        private Pool<GameObject> _pool;
        private EnemyManager _enemyManager;
        private IFightService _fightService;

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
                if (gameObject.GetComponent<ParticleRemover>())
                {
                    gameObject.GetComponent<ParticleRemover>().Initialize(_fightService);
                }
                gameObject.transform.position = movePointPositions[i];
                _enemyManager.StartCoroutine(_pool.AddToPoolLatter(gameObject, _data.Lifetime));
            }
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _pool = new Pool<GameObject>(_data.ParticleGO, _data.Count);
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
            _fightService = (IFightService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.FightService];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}