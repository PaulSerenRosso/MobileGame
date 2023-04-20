using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/IntInternValueNodeSO",
        fileName = "new  CH_IntInternValue_Spe")]
    public class CheckIntInternValueNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Int(int) to check in intern values");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de vérifier un int dans les intern values";
        }
    }
}