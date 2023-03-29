using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/PlaySoundOneShotNodeSO", fileName = "new T_PlaySounfOneShot_Spe")]
    public class TaskPlaySoundOneShotNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de jouer un son";
        }
    }
}