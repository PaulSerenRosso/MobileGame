using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/InternTimerNodeSO", fileName = "new  CH_InternTimer_Spe")]
    public class CheckInternTimerNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Timer(Float) the node need to wait");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de retourner un succès selon un timer";
        }
    }
}