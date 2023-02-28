using BehaviorTree;
using UnityEngine;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/TaskGetIndexMovePointPositionNodeSO",
        fileName = "new TaskGetIndexMovePointPositionNodeSO")]
    public class TaskGetIndexMovePointPositionNodeSO : ActionNodeSO
    {
        public StringWithHashCode IndexMovePointKey;
        public StringWithHashCode DestinationIndexMovePointKey;

        public override void ConvertKeyOfInternValueToHashCode()
        {
            IndexMovePointKey.UpdateKeyHashCode();
            DestinationIndexMovePointKey.UpdateKeyHashCode();
        }
    }
}