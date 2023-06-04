using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/LookDirectionNodeSO", fileName = "new Tree_T_LookDirection_Spe")]
    public class TaskLookDirectionNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Direction(Vector3) where the boss need to look");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de tourner le boss vers une direction";
        }
    }
}