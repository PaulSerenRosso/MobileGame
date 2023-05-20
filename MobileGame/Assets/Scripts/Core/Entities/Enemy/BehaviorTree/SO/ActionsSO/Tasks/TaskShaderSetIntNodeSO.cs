using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderSetIntNodeSO",
        fileName = "new Tree_T_ShaderSetInt_Spe")]
    public class TaskShaderSetIntNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur bool d'un Shader";
        }
    }
}