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
        TickManager,
        PlayerHandlerMovement,
        PoolService
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public enum InternValueType
    {
        VECTOR3, INT, FLOAT, NONE
    }

    public enum InternValuePropertyType
    {
        GET, SET, REMOVE
    }
}