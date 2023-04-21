using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/TimerNodeSO", fileName = "new CH_Timer_Spe")]
    public class CheckTimerNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.CALLBACK,
                    BehaviorTreeEnums.InternValuePropertyType.SET, "set the function to reset timer manually, removed automatically when timer is ended");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                        BehaviorTreeEnums.InternValuePropertyType.GET, "Float(float) get timer to use in intern value");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de retourner un succès selon un timer";
        }
    }
}