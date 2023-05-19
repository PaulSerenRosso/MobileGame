using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetRandomMovePointsNodeSO",
        fileName = "new Tree_T_GetRandomMovePoints_Spe")]
    public class TaskGetRandomMovePointsNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Index(int) how many movePoints to take randomly");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3LIST,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Positions(Vector3[]) random");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de choisir un nombre alétoire de position de random";
        }
    }
}