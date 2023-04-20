using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/RemapNodeSO", fileName = "new T_Remap_Spe")]
    public class TaskRemapNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Float(float) get float in intern values to remap");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Int(int) set int in intern values to remap");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de remap une valeur entre des valeurs renseignées";
        }
    }
}