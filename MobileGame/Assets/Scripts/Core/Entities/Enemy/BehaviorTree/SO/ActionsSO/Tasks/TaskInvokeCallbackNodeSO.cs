using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/InvokeCallbackNodeSO",
        fileName = "new T_InvokeCallback_Spe")]
    public class TaskInvokeCallbackNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.CALLBACK,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Get the function to invoke");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Permet de déclencher un callback préablement SET";
        }
    }
}