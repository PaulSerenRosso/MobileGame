using System;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/CheckTimerDataSO", fileName = "new CheckTimerDataSO")]
    public class CheckTimerDataSO : ActionNodeDataSO
    {
        public float Time;
        public float StartTime;

        public override Type GetTypeNode()
        {
            return typeof(CheckTimer);
        }

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.ITickService };
        }
    }
}