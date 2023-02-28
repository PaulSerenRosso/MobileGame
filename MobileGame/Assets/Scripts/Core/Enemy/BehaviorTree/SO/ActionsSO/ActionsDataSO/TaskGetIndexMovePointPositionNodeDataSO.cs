using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/TaskGetIndexMovePointPositionNodeDataSO", fileName = "new TaskGetIndexMovePointPositionNodeDataSO")]
    public class TaskGetIndexMovePointPositionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetIndexMovePointPositionNode);
        }
    }
}