using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetPlayerDirectionNodeSO", fileName = "new Tree_T_GetPlayerDirection_Spe")]
    public class TaskGetPlayerDirectionNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Index(int) where the player is");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Direction(Vector3) where the boss need to look");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de récupérer la direction vers le joueur";
        }
    }
}