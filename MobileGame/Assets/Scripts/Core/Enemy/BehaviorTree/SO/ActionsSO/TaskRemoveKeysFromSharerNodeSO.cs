using BehaviorTree;
using UnityEngine;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/TaskRemoveKeysFromSharerNodeSO", fileName = "new TaskRemoveKeysFromSharerNodeSO")]
    public class TaskRemoveKeysFromSharerNodeSO : ActionNodeSO
    {
        public StringWithHashCode[] keysToRemove;

        public override void ConvertKeyOfInternValueToHashCode()
        {
            for (int i = 0; i < keysToRemove.Length; i++)
            {
                keysToRemove[i].UpdateKeyHashCode();
            }
        }
    }
}