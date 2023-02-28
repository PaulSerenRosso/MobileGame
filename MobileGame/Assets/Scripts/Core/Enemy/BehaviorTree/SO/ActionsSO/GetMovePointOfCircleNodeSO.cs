using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/GetMovePointOfCircleNodeSO",
        fileName = "new  GetMovePointOfCircleNodeSO")]
    public class GetMovePointOfCircleNodeSO : ActionNodeSO
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