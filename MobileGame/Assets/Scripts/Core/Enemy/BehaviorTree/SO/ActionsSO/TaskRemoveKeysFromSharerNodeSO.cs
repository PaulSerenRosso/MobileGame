using BehaviorTree;
using UnityEngine;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/TaskRemoveKeysFromSharerNodeSO",
        fileName = "new TaskRemoveKeysFromSharerNodeSO")]
    public class TaskRemoveKeysFromSharerNodeSO : ActionNodeSO
    {
        public StringWithHashCode[] KeysToRemove;

        public override void ConvertKeyOfInternValueToHashCode()
        {
            for (int i = 0; i < KeysToRemove.Length; i++)
            {
                KeysToRemove[i].UpdateKeyHashCode();
            }
        }
    }
}