using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderSetFloatLerpNodeSO",
        fileName = "new Tree_T_ShaderSetFloatLerp_Spe")]
    public class TaskShaderSetFloatLerpNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur float d'un Shader avec un LERP";
        }
    }
}