using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetIndexMovePointPositionNode : ActionNode
    {
        private TaskGetIndexMovePointPositionNodeSO _so;
        private TaskGetIndexMovePointPositionNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetIndexMovePointPositionNodeSO)nodeSO;
            _data = (TaskGetIndexMovePointPositionNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            int index = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _environmentGridManager.MovePoints[index].MeshRenderer.transform.position);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}