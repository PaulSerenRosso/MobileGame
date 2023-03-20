using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/MoveGridNodeDataSO", fileName = "new T_MoveGrid_Spe_Data")]
    public class TaskMoveGridNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager,
                BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveGridNode);
        }
    }
}