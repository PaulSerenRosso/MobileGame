using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/StateDataSO", fileName = "new CH_State_Spe_Data")]
    public class CheckStateNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyState EnemyState;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckStateNode);
        }
    }
}