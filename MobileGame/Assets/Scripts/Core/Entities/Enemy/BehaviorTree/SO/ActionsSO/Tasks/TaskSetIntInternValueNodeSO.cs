using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetIntInternValueNodeSO",
        fileName = "new Tree_T_SetIntInternValue_Spe")]
    public class TaskSetIntInternValueNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 3;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Int(int) value to get from intern value");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Int(int) value to set in intern value");
                    if (InternValues.Count > 2)
                    {
                        InternValues[2].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                            BehaviorTreeEnums.InternValuePropertyType.GET, "Float(float) value to get from intern value");
                    }
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de SET une valeur integer dans les interns values";
        }
    }
}