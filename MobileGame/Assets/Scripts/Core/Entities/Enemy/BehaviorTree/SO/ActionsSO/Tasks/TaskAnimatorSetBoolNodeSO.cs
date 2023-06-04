using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/AnimatorSetBoolNodeSO",
        fileName = "new Tree_T_AnimatorSetBool_Spe")]
    public class TaskAnimatorSetBoolNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.BOOL,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Value(bool) of a parameter");
            }
        }
        
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de modifier la valeur bool d'un paramètre dans un animator";
        }
    }
}