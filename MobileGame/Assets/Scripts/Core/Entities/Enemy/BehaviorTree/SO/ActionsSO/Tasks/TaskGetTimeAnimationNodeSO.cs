using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetTimeAnimationNodeSO", fileName = "new Tree_T_GetTimeAnimation_Spe")]
    public class TaskGetTimeAnimationNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "Timer(float) the time of the current animation");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui récupère le temps de l'animation actuelle";
        }
    }
}