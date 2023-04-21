using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/LerpHypeNodeSO",
        fileName = "new  T_LerpHype_Spe")]
    public class TaskLerpHypeNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Float(float) set ratio into intern values");
            }
        }
        
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne le ratio de hype actuel";
        }
    }
}