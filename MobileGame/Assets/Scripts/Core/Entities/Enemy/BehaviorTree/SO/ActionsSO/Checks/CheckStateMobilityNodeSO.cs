using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/StateMobilityNodeSO",
        fileName = "new  Tree_CH_StateMobility_Spe")]
    public class CheckStateMobilityNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Vérifier que la state actuelle correspond à la state renseignée";
        }
    }
}