using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SendPlayerMovePointIndexNodeSO",
        fileName = "new T_SendPlayerMovePointIndex_Spe")]
    public class TaskSendPlayerMovePointIndexNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.SET, "Index(int) of the movepoint where the player is");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet d'enregistrer la position du joueur durant une partie";
        }
    }
}