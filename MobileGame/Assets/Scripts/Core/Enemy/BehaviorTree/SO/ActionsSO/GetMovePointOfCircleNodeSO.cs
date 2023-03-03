using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/GetMovePointOfCircleNodeSO",
        fileName = "new  GetMovePointOfCircleNodeSO")]
    public class GetMovePointOfCircleNodeSO : ActionNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Index(int) of the MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                        BehaviourTreeEnums.InternValuePropertyType.SET, "Index(int) of a MovePoint after the movement");
                }
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}