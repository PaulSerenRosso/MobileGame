using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/UltimateHypeNodeSO", fileName = "new Tree_CH_UltimateHype_Spe")]
    public class CheckUltimateHypeNodeSO: CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Invoke l'ultimate du boss si la hype est en sa faveur";
        }
    }
}