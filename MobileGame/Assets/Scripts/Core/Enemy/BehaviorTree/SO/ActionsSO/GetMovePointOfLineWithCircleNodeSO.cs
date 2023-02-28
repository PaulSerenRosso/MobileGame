using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/GetMovePointOfLineWithCircleNodeSO",
        fileName = "new  GetMovePointOfLineWithCircleNodeSO")]
    public class GetMovePointOfLineWithCircleNodeSO : ActionNodeSO
    {
        public StringWithHashCode StartIndexKey = new();
        public StringWithHashCode ResultIndexKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            StartIndexKey.UpdateKeyHashCode();
            ResultIndexKey.UpdateKeyHashCode();
        }
    }
}