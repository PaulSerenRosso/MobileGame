using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/AnimatorPlayAnimationNodeSO",
        fileName = "new Tree_T_AnimatorPlayAnimation_Spe")]
    public class TaskAnimatorPlayAnimationNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de jouer une animation dans un animator";
        }
    }
}