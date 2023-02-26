using System;
using BehaviorTree.Nodes;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree.ActionsSO
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