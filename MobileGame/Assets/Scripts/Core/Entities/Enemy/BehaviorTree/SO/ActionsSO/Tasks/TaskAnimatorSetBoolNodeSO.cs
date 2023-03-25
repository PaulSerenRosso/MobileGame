using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/AnimatorSetBoolNodeSO",
        fileName = "new T_AnimatorSetBool_Spe")]
    public class TaskAnimatorSetBoolNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de modifier la valeur bool d'un paramètre dans un animator";
        }
    }
}