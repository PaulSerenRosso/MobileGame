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
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GETANDSET, "Float(float) value get to increase");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui un float pour l'increase";
        }
    }
}