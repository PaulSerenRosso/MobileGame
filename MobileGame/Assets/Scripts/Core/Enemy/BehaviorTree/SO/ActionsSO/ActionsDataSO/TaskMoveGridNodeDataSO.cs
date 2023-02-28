using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/TaskMoveGridNodeDataSO", fileName = "new TaskMoveGridNodeDataSO")]
    public class TaskMoveGridNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager, BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveGridNode);
        }
    }
}