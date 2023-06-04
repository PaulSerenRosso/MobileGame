using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetStringNodeSO",
        fileName = "new Tree_T_SetString_Spe")]
    public class TaskSetStringNodeSO: TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.STRING,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "string(string) value to get from intern value");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui set la valeur d'un string en intern value";
        }
    }
}