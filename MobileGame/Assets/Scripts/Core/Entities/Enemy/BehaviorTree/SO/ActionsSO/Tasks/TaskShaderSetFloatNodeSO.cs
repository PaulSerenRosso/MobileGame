using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ShaderSetFloatNodeSO",
        fileName = "new Tree_T_ShaderSetFloat_Spe")]
    public class TaskShaderSetFloatNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.FLOAT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "float(float) value to get from intern value");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.STRING,
                        BehaviorTreeEnums.InternValuePropertyType.GET, "String(string) value to get from intern value");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie la valeur float d'un Shader";
        }
    }
}