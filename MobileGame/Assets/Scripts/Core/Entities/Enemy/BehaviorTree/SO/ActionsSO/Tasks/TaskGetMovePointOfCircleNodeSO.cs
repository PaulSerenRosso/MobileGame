using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetMovePointOfCircleNodeSO",
        fileName = "new  Tree_T_GetMovePointOfCircle_Spe")]
    public class TaskGetMovePointOfCircleNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Index(int) of the MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Index(int) of a MovePoint after the movement");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Retourner un point selon un déplacement sur le cercle du point renseigné";
        }
    }
}