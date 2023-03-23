namespace BehaviorTree
{
    public class TaskGetPlayerDirectionNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.VECTOR3,
                    BehaviourTreeEnums.InternValuePropertyType.SET, "Direction(Vector3) where the boss need to look");
            }
        }

        public override void UpdateComment()
        {
            Comment = "";
        }
    }
}