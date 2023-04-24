using UnityEngine;

namespace BehaviorTree
{
        [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SendPlayerPositionNodeSO",
            fileName = "new Tree_T_SendPlayerPosition_Spe")]
    public class TaskSendPlayerPositionNodeSO : TaskNodeSO
    {
     
            public override void UpdateInterValues()
            {
                base.UpdateInterValues();
                _internValuesCount = 1;
                if (InternValues.Count > 0)
                {
                    InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                        BehaviorTreeEnums.InternValuePropertyType.SET, "Position of the player is");
                }
            }

            public override void UpdateComment()
            {
                Comment = "Nœud qui permet d'enregistrer la position du joueur durant une partie";
            }
        }
    }
    
