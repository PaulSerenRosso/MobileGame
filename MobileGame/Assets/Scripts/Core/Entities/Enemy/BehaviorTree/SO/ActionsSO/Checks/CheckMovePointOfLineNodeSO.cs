using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/MovePointOfLineNodeSO", fileName = "new  CH_MovePointOfLine_Spe")]
    public class CheckMovePointOfLineNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "INT(Int) the node to compare");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.GET,
                        "INT(Int) the node to compare");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de comparer deux index pour regarder s'ils sont sur la même ligne";
        }
    }
}