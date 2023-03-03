using BehaviorTree;
using UnityEngine;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/TaskRemoveKeysFromSharerNodeSO",
        fileName = "new TaskRemoveKeysFromSharerNodeSO")]
    public class TaskRemoveKeysFromSharerNodeSO : ActionNodeSO
    {
   

        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount =(byte) InternValues.Count;
            foreach (var internValue in InternValues)
            {
                internValue.SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.NONE,
                    BehaviourTreeEnums.InternValuePropertyType.REMOVE, "To Removed");
            }
           
        }

        public override void UpdateCommentary()
        {
        }
    }
}