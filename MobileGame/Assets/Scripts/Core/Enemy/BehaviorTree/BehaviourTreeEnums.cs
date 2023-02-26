public class BehaviourTreeEnums
{
    public enum TreeEnemyValues
    {
        Rigidbody,
        Animator,
        Transform,
        MeshRenderer
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        EnvironmentGridManager,
        ITickService
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}