using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/TimerNodeSO", fileName = "new CH_Timer_Spe")]
    public class CheckTimerNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.CALLBACK,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "set the function to reset timer manually, removed automatically when timer is ended");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de retourner un succès selon un timer";
        }
    }
}