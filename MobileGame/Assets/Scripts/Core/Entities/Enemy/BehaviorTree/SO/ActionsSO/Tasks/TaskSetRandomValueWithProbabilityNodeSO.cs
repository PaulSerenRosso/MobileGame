using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetRandomValueWithProbabilityNodeSO",
        fileName = "new T_SetRandomValueWithProbability_Spe")]
    public class TaskSetRandomValueWithProbabilityNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            var _data = (TaskSetRandomValueWithProbabilityNodeDataSO)Data;
            _internValuesCount = (byte)(_data.StartProbabilitiesValues.Length + 2);
            for (int i = 0; i < _internValuesCount; i++)
            {
                if (InternValues.Count > i)
                    InternValues[i].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.GETANDSET, "int(int) value to set in intern value");
            }

            if (InternValues.Count > _internValuesCount - 2)
                InternValues[_internValuesCount - 2].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "int(int) value to set in intern value");
            
            if (InternValues.Count > _internValuesCount - 1)
                InternValues[_internValuesCount - 1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.CALLBACK,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "callback(callback) value to set in intern value");
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet d'ajouter des probabilités modifiables dans les intern values";
        }
    }
}