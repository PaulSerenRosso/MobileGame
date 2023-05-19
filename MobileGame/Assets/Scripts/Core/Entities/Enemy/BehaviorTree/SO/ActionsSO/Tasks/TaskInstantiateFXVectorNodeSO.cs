using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/InstantiateFXVectorNodeSO",
        fileName = "new Tree_T_InstantiateFXVector_Spe")]
    public class TaskInstantiateFXVectorNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3LIST,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Vector3(List<Vector3>()) vector3 positions");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet d'instancier des FXs aux positions";
        }
    }
}