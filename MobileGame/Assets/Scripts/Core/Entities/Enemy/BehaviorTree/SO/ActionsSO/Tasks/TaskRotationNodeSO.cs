using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/RotationNodeSO", fileName = "new Tree_T_RotationNode_Spe")]
    public class TaskRotationNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Fait tourner notre ennemi en fonction de la rotation renseignée dans le DATA";
        }
    }
}