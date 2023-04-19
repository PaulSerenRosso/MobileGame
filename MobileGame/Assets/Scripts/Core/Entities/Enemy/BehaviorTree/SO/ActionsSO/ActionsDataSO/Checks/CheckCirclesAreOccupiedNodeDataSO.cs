using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/CirclesAreOccupiedNodeDataSO",
        fileName = "new CH_CirclesAreOccupied_Spe_Data")]
    public class CheckCirclesAreOccupiedNodeDataSO : ActionNodeDataSO
    {
        public int[] CircleIndexes;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckCirclesAreOccupiedNode);
        }
    }
}