using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderSetFloatNodeSO",
        fileName = "new T_ShaderSetFloat_Spe")]
    public class TaskShaderSetFloatNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur float d'un Shader";
        }
    }
}