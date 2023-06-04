using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetMovePointOfLineNodeSO",
        fileName = "new  Tree_T_GetMovePointOfLine_Spe")]
    public class TaskGetMovePointOfLineNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Index(int) of a MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.SET,
                        "Index(int) of a MovePoint after the movement");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Retourne un déplacement sur une ligne / colonne de cercle";
        }
    }
}