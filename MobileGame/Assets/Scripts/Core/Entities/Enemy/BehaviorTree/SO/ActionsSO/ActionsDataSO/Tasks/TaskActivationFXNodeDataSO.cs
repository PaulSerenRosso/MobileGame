using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ActivationFXNodeDataSO",
        fileName = "new Tree_T_ActivationFX_Spe_Data")]
    public class TaskActivationFXNodeDataSO : ActionNodeDataSO
    {
        public BehaviorTreeEnums.TreeEnemyValues[] FXEnumValues;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = FXEnumValues;
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskActivationFXNode);
        }
    }
}