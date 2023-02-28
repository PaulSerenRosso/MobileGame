using BehaviorTree;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    public class GetMovePointOfLineNodeSO : ActionNodeSO
    {
        public StringWithHashCode StartIndexKey;
        public StringWithHashCode ResultIndexKey;

        public override void ConvertKeyOfInternValueToHashCode()
        {
            StartIndexKey.UpdateKeyHashCode();
            ResultIndexKey.UpdateKeyHashCode();
        }
    }
}