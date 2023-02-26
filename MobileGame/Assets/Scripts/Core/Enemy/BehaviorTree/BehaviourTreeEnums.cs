public class BehaviourTreeEnums
{
    public enum TreeEnemyValues
    {
        Rigidbody,
        Animator,
        Transform
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        EnvironmentGridManager,
        ITickService
    }

    public enum TreeInternValues
    {
        
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}