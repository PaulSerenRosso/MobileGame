using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/CirclesPlayerNodeSO", fileName = "new Tree_CH_CirclesPlayer_Spe")]
    public class CheckCirclesPlayerNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Retourne si notre joueur est actuellement dans le rang de points concerné";
        }
    }
}