using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/DamagePlayerNodeDataSO",
        fileName = "new T_DamagePlayer_Spe_Data")]
    public class TaskDamagePlayerNodeDataSO : ActionNodeDataSO
    {
        public float Damage;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHealth };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskDamagePlayerNode);
        }
    }
}