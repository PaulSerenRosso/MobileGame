namespace BehaviorTree
{
    public class TaskShaderSetFloatLerpNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur float d'un Shader avec un LERP";
        }
    }
}