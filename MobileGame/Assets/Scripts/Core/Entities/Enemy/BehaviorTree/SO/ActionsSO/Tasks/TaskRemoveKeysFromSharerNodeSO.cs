using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/RemoveKeysFromSharerNodeSO",
        fileName = "new Tree_T_RemoveKeysFromSharer_Spe")]
    public class TaskRemoveKeysFromSharerNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = (byte)InternValues.Count;
            foreach (var internValue in InternValues)
            {
                internValue.SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.NONE,
                    BehaviorTreeEnums.InternValuePropertyType.REMOVE, "To Removed");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de supprimer les valeurs précédentes enregistrer sur le blackboard";
        }
    }
}