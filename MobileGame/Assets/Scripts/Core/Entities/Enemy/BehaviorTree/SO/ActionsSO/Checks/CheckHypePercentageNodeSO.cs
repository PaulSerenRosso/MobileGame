using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/HypePercentageNodeSO",
        fileName = "new Tree_CH_HypePercentage_Spe")]
    public class CheckHypePercentageNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne une réussite si la comparaison sur la hype est exacte";
        }
    }
}