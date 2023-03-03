using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/GetMovePointOfLineWithCircleNodeSO",
        fileName = "new  GetMovePointOfLineWithCircleNodeSO")]
    public class GetMovePointOfLineWithCircleNodeSO : ActionNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Index(int) of a MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                        BehaviourTreeEnums.InternValuePropertyType.SET,
                        "Index(int) of a MovePoint where the circle index is pointed in data");
                }
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}