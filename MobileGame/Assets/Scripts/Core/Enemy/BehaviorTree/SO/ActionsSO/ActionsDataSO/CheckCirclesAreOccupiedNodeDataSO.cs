using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/CheckCirclesAreOccupiedNodeDataSO", fileName = "new CheckCirclesAreOccupiedNodeDataSO")]
    public class CheckCirclesAreOccupiedNodeDataSO : ActionNodeDataSO
    {
        public int[] CircleIndexes;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckCirclesAreOccupiedNode);
        }
    }
}