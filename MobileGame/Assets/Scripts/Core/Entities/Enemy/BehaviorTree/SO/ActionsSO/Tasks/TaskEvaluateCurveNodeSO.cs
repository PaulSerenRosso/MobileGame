using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/EvaluateCurveNodeSO",
        fileName = "new Tree_T_EvaluateCurve_Spe")]
    public class TaskEvaluateCurveNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Float(float) get value float in intern values");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                        BehaviorTreeEnums.InternValuePropertyType.SET,
                        "float(float) set value of float into intern values");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de déterminer une probabilité selon un ratio obtenu";
        }
    }
}