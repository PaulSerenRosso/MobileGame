using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/RandomRangeNodeSO", fileName = "new T_RandomRange_Spe")]
    public class TaskRandomRangeNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Float(float) get float in intern values to remap");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de renvoyer un random entre deux valeurs renseignées";
        }
    }
}