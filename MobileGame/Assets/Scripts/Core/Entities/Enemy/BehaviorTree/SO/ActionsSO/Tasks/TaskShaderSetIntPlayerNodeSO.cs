using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderSetIntPlayerNodeSO",
        fileName = "new Tree_T_ShaderSetIntPlayer_Spe")]
    public class TaskShaderSetIntPlayerNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur int d'un Shader";
        }
    }
}