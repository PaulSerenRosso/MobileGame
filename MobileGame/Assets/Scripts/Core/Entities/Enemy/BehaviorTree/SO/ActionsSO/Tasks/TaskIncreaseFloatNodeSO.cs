using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/IncreaseFloatNodeSO",
        fileName = "new Tree_T_IncreaseFloat_Spe")]
    public class TaskIncreaseFloatNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Float(float) value get to increase");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Float(float) value set");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui un float pour l'increase";
        }
    }
}