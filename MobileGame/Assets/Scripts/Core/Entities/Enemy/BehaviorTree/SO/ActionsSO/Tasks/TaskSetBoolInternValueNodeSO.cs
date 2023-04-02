using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetBoolInternValueNodeSO",
        fileName = "new T_SetBoolInternValue_Spe")]
    public class TaskSetBoolInternValueNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.BOOL,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Boolean(bool) value to set in intern value");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de SET une valeur booléene dans les interns values";
        }
    }
}