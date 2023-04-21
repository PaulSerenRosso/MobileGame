using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/LookDirectionDirectNodeSO", fileName = "new T_LookDirectionDirect_Spe")]
    public class TaskLookDirectionDirectNodeSO : TaskNodeSO
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
            Comment = "Nœud qui permet de tourner le boss vers une direction directement";
        }
    }
}