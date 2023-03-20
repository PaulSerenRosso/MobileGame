namespace BehaviorTree
{
    public class TaskInstantiateFXNodeSO : ActionNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "MovePoint(Int) index of position");
            }
        }

        public override void UpdateComment()
        {
            Comment = "";
        }
    }
}