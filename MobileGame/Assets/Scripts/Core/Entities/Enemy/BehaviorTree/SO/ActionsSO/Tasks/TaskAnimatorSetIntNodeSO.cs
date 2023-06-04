using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/AnimatorSetIntNodeSO",
        fileName = "new Tree_T_AnimatorSetInt_Spe")]
    public class TaskAnimatorSetIntNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Value(int) of a parameter");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de modifier la valeur int d'un paramètre dans un animator";
        }
    }
}