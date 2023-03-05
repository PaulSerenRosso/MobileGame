using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/RemoveKeysFromSharerNodeSO",
        fileName = "new T_RemoveKeysFromSharer_Spe")]
    public class TaskRemoveKeysFromSharerNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = (byte)InternValues.Count;
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