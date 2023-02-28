using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/TaskMoveGridNodeSO", fileName = "new TaskMoveGridNodeSO")]
    public class TaskMoveGridNodeSO : ActionNodeSO
    {
        public StringWithHashCode DestinationKey = new();
        public StringWithHashCode DestinationIndexMovePointKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            DestinationKey.UpdateKeyHashCode();
        }
    }
}