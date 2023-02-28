using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/TaskMoveNodeSO", fileName = "new TaskMoveNodeSO")]
    public class TaskEnemyMoveNodeSO : ActionNodeSO
    {
        public StringWithHashCode DestinationKey = new();
        
        public override void ConvertKeyOfInternValueToHashCode()
        {
            DestinationKey.UpdateKeyHashCode();
        }
    }
}