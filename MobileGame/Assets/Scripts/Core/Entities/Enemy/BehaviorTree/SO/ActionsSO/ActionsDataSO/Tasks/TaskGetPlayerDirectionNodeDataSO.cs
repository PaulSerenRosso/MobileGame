using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetPlayerDirectionNodeDataSO",
        fileName = "new T_GetPlayerDirection_Spe_Data")]
    public class TaskGetPlayerDirectionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetPlayerDirectionNode);
        }
    }
}