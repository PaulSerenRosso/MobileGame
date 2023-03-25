using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/TimerNodeSO", fileName = "new CH_Timer_Spe")]
    public class CheckTimerNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de retourner un succès selon un timer";
        }
    }
}