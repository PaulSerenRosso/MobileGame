using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/StateDataSO", fileName = "new CH_State_Spe_Data")]
    public class CheckStateNodeDataSO : ActionNodeDataSO
    {
        [FormerlySerializedAs("EnemyState")] public EnemyEnums.EnemyMobilityState enemyMobilityState;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckStateNode);
        }
    }
}