using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/TaskMoveNodeDataSO", fileName = "new TaskMoveNodeDataSO")]
    public class TaskMoveNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveNode);
        }
    }
}