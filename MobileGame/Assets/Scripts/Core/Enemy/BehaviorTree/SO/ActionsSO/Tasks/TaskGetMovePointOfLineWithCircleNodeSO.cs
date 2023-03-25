using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetMovePointOfLineWithCircleNodeSO",
        fileName = "new  T_GetMovePointOfLineWithCircle_Spe")]
    public class TaskGetMovePointOfLineWithCircleNodeSO : TaskNodeSO
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
                        "Index(int) of a MovePoint where the circle index is pointed in data");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Retourner un point selon la position du joueur et le cercle de points visé";
        }
    }
}