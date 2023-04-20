using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/RatioHypeNodeSO",
        fileName = "new  CH_RatioHype_Spe")]
    public class CheckRatioHypeNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Float(float) set ratio into intern values");
            }
        }
        
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne le ratio de hype actuel";
        }
    }
}