using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/MoveGridNodeDataSO", fileName = "new Tree_T_MoveGrid_Spe_Data")]
    public class TaskMoveGridNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.GridManager,
                BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveGridNode);
        }
    }
}