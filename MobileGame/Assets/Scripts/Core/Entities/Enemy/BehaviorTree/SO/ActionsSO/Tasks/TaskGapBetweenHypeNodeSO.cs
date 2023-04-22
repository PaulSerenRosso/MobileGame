using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GapBetweenHypeNodeSO",
        fileName = "new  Tree_T_GapBetweenHype_Spe")]
    public class TaskGapBetweenHypeNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Float(float) set gap into intern values");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne le ratio de hype actuel";
        }
    }
}