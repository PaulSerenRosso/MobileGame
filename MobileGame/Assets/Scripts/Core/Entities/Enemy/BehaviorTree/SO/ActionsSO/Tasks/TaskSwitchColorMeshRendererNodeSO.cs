using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SwitchColorMeshRendererNodeSO",
        fileName = "new Tree_T_SwitchColorMeshRenderer_Spe")]
    public class TaskSwitchColorMeshRendererNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de modifier la couleur du joueur";
        }
    }
}