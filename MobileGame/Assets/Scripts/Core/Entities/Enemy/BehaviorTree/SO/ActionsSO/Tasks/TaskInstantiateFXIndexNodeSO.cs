using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/InstantiateFXIndexNodeSO",
        fileName = "new Tree_T_InstantiateFXIndex_Spe")]
    public class TaskInstantiateFXIndexNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "MovePoint(Int) index of position");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet d'instancier un FX";
        }
    }
}