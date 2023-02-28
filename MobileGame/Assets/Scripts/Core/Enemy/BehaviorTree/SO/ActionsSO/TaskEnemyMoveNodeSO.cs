using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/TaskMoveNodeSO", fileName = "new TaskMoveNodeSO")]
    public class TaskEnemyMoveNodeSO : ActionNodeSO
    {
        public StringWithHashCode DestinationKey = new();

        
        public override void ConvertKeyOfInternValueToHashCode()
        {
            DestinationKey.UpdateKeyHashCode();
        }
    }
}