using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/CheckCirclesAreOccupiedNodeSO", fileName = "new CheckCirclesAreOccupiedNodeSO")]
    public class CheckCirclesAreOccupiedNodeSO : ActionNodeSO
    {
        public StringWithHashCode DestinationKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            DestinationKey.UpdateKeyHashCode();
        }
    }
}