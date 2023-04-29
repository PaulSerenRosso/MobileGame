using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderDamageSetIntPlayerNodeSO",
        fileName = "new Tree_T_ShaderDamageSetIntPlayer_Spe")]
    public class TaskShaderDamageSetIntPlayerNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur int d'un Shader";
        }
    }
}