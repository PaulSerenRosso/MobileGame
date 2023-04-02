using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerDirectionNodeSO",
        fileName = "new  CH_PlayerDirection_Spe")]
    public class CheckPlayerDirectionNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Direction(Vector3) where the boss need to look");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de vérifier si le boss regarde bien dans la direction du joueur";
        }
    }
}