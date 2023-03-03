using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/SendPlayerMovePointIndexNodeSO",
        fileName = "new SendPlayerMovePointIndexNodeSO")]
    public class SendPlayerMovePointIndexNodeSO : ActionNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.SET, "Index(int) of the movepoint where the player is");
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}