using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetMovePointOfLineNodeSO",
        fileName = "new  T_GetMovePointOfLine_Spe")]
    public class TaskGetMovePointOfLineNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Index(int) of a MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                        BehaviourTreeEnums.InternValuePropertyType.SET,
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