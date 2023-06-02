using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/InstantiateFXVectorNodeDataSO",
        fileName = "new Tree_T_InstantiateFXVector_Spe_Data")]
    public class TaskInstantiateFXVectorNodeDataSO : ActionNodeDataSO
    {
        public GameObject ParticleGO;
        public int Count;
        public float Lifetime;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.FightService };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskInstantiateFXVectorNode);
        }
    }
}