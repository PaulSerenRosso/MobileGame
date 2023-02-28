using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/SendPlayerMovePointIndexNodeSO",
        fileName = "new SendPlayerMovePointIndexNodeSO")]
    public class SendPlayerMovePointIndexNodeSO : ActionNodeSO
    {
        public StringWithHashCode PlayerMovePointIndexKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            PlayerMovePointIndexKey.UpdateKeyHashCode();
        }
    }
}